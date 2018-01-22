/* 定义全局对象，类似于命名空间或包的作用 */
var Utils = $.extend({}, Utils);

/* 增加命名空间功能,使用方法：Utils.NameSpace('jQuery.bbb.ccc','Home.Main'); */
Utils.NameSpace = function () {
    var o = {}, d;
    for (var i = 0; i < arguments.length; i++) {
        d = arguments[i].split(".");
        o = window[d[0]] = window[d[0]] || {};
        for (var k = 0; k < d.slice(1).length; k++) {
            o = o[d[k + 1]] = o[d[k + 1]] || {};
        }
    }
    return o;
};