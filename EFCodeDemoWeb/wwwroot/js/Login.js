Utils.NameSpace('Account.Login');
;(function (window, document, $) {
    $(function () {
        // JS严格模式
        'use strict';
        //Account.Login.LoginFrist = true;
        // 页面初始化
        Account.Login.Init();
        setMaxDigits(130);
        $("#validcode").val("");
        if (parseInt($("#LoginErrorTime").text()) > 0) {
            $('#validateCodeDiv').show();
        }
    });

    Account.Login.Init = function () {
        $('#validateCodeDiv').hide();
        $('#erroralert').hide();
        Account.Login.ValidationForm();
        Account.Login.ValidateCodeClick();
        Account.Login.UserValidationExist();
        $('.reload-vify').click();//提前获验证码
    };

    Account.Login.ValidationForm = function () {
        $("#loginForm").formValidation({
            locale: 'zh_CN',
            framework: 'bootstrap',
            excluded: ':disabled',
            icon: {
                valid: 'icon wb-check',
                invalid: 'icon wb-close',
                validating: 'icon wb-refresh'
            },
            fields: {
                userName: {
                    validators: {
                        notEmpty: {
                            message: '用户名不能为空'
                        },
                        remote: {
                            type: 'GET',
                            url: "/Account/LoginNameExist",
                            data: function (validator, $field, value) {
                                return {
                                    userName: $("#userName").val()
                                };
                            },
                            success: function (data) {
                                if (data.exist == true) {
                                    set_Exist_value(1, ValidateCodeControl(true));
                                } else {
                                    set_Exist_value(0, ValidateCodeControl(false));
                                }
                            },
                            message: '此用户不存在',
                            delay: 1000
                        }
                        //callback:{
                        //    callback: function(validator, $field, options){
                        //        //if(UserExist == 0){
                        //        //    return false;
                        //        //}else{
                        //        //    return true;
                        //        //}
                        //    }
                        //}
                    }
                },
                input_password: {
                    validators: {
                        notEmpty: {
                            message: '密码不能为空'
                        },
                        stringLength: {
                            min: 5,
                            max: 32,
                            message: '密码必须大于5且小于30个字符'
                        }
                    }
                },
                validcode: {
                    validators: {
                        notEmpty: {
                            enabled: true,
                            message: '验证码不能为空'
                        }
                    }
                }
            }
        })
        .on('success.form.fv', function (e, data) {
            // 验证通过后重置错误信息提示
            if($('#input_password').val() != null && $('#input_password').val() != ""){
                Account.Login.ErrorAlertClear(true);
                var userPwd = $('#input_password').val();
                var e = $('#Exponent').text();
                var m = $('#Modulus').text();
                var key = new RSAKeyPair(e, "", m);
                var key2 = new RSAKeyPair(e, "", m);
                var ePWD = encryptedString(key, encodeURI(userPwd));
                $('#password').val(ePWD);
            }
        })
        .on('err.field.fv', function (e, data) {
            $('#erroralert').show();
            // 如果已存在，删除字段消息
            $('#erroralert').find('li[data-field="' + data.field + '"]').remove();
            // 获取字段消息
            var messages = data.fv.getMessages(data.element);
            if(messages != null){
                $('#erroralert').html(messages[0]);
            }
        });
    };
    
    Account.Login.ValidateCodeClick = function () {
        $('.reload-vify').on('click', function () { // 验证码刷新
            var $img = $(this).children('img'),
                URL = "/Account/CheckCode";//$img.prop('src');

            $img.prop('src', URL + '?userid=' + $('#userName').val() + '&tm=' + Math.random());
        });
    };

    Account.Login.UserValidationExist = function () {
        $('#userName').on('blur', function () {
            if ($('#userName').val() == null || $('#userName').val() == "" || $('#userName').val() == undefined) {
                return;
            }
            $('#validateCodeDiv').show();
        });
        $('#input_password').on('change', function () {
            if ($('#input_password').val() == null || $('#input_password').val() == "" || $('#input_password').val() == undefined) {
                return;
            }
            if ($('#userName').val() == null || $('#userName').val() == "" || $('#userName').val() == undefined) {
                return;
            }
            $('#validateCodeDiv').show();
        });
    };

    Account.Login.ErrorAlertClear = function (isHide) {
        $('#erroralert').html('');
        if(isHide == true){
            $('#erroralert').hide();
        }
    }

    Account.Login.Submit = function (Form) {
        var $item = $(Form);

        $.ajax({
            url: '/Account/Login',
            type: 'POST',
            data: $item.serialize(),
            dataType: 'JSON',
            success: function (data) {
                if (data.success) {

                    $('#loginForm').formValidation('resetForm', true);//重置校验
                    $(':input', '#loginForm')//清空表单
                        .not(':button, :submit, :reset, :hidden')
                        .val('')
                        .removeAttr('checked')
                        .removeAttr('selected');
                    toastr.success(data.msg);
                } else {
                    toastr.error(data.msg);
                }
            },
            error: function () {
                toastr.error('服务器异常，请稍后再试！');
            }
        });
    }
})(window, document, jQuery);

