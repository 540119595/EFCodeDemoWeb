using Common.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.Filters
{
    public class GlobalActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext actionExecutedContext)
        {
            // 在执行操作方法之前调用
        }

        public void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            // 在执行操作方法后调用
            var request = actionExecutedContext.HttpContext.Request;
            try
            {
                string username = "访客";
                string parameter = "暂时未取到";// request.Form.Keys.Count > 0 ? request.Form.ToString() : "";
                //request.Form.Count > 0 ? request.Form.ToString() : "";
                //parameter += request.QueryString.ToString ?? "，请求信息：" + request.QueryString.ToString() : "";
                string reqMsg = string.Format("用户：{0}，传输方式：{1}，请求路径：{2}，表单信息：{3}，上一路径：{4}。",
                        username, request.Method, request.Path, parameter, request.Headers["Referer"]);


                Log4NetHelper.WriteInfo(actionExecutedContext.Controller.GetType(), reqMsg);
            }
            catch (System.InvalidOperationException ex)
            {
                //string username = "访客";
                //string parameter = request.Form.Keys.Count > 0 ? request.Form.ToString() : "";
                ////request.Form.Count > 0 ? request.Form.ToString() : "";
                ////parameter += request.QueryString.ToString ?? "，请求信息：" + request.QueryString.ToString() : "";
                //string reqMsg = string.Format("用户：{0}，传输方式：{1}，请求路径：{2}，表单内容：{3}，上一路径：{4}，异常信息：{5}",
                //    username, request.Method, request.Path, request.Unvalidated.Form, request.UrlReferrer, ex.Message);
                
                //Log4NetHelper.WriteInfo(actionExecutedContext.Controller.GetType(), reqMsg);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
