using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace WebUI.Common
{
    public static class HMTLHelperExtensions
    {
        public static string IsActive(this IHtmlHelper html, string controller = null, string action = null)
        {
            string activeClass = "active"; // change here if you another name to activate sidebar items
            // detect current app state
            string actualAction = (string)html.ViewContext.RouteData.Values["action"];
            string actualController = (string)html.ViewContext.RouteData.Values["controller"];

            if (string.IsNullOrEmpty(controller))
                controller = actualController;

            if (string.IsNullOrEmpty(action))
                action = actualAction;

            return (controller == actualController && action == actualAction) ? activeClass : string.Empty;
        }

        public static IHtmlContent DropCheckBoxFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper
           , Expression<Func<TModel, TProperty>> expression
           , object htmlAttributes)
        {
            IDictionary<bool, string> YesOrNo = new Dictionary<bool, string>
            {
                { true, "Sim" },
                { false, "Não" }
            };

            return htmlHelper.DropDownListFor(expression, new SelectList(YesOrNo, "Key", "Value"), htmlAttributes);
        }

        public static IHtmlContent DropLanguageCheckBoxFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper
           , Expression<Func<TModel, TProperty>> expression
           , object htmlAttributes)
        {
            IDictionary<bool, string> YesOrNo = new Dictionary<bool, string>
            {
                { true, "Português" },
                { false, "Inglês" }
            };

            return htmlHelper.DropDownListFor(expression, new SelectList(YesOrNo, "Key", "Value"), htmlAttributes);
        }

        public static IHtmlContent DropCheckBoxForVoltage<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper
           , Expression<Func<TModel, TProperty>> expression
           , object htmlAttributes)
        {
            IDictionary<string, string> YesOrNo = new Dictionary<string, string>
            {
                { "110", "110" },
                { "220", "220" }
            };

            return htmlHelper.DropDownListFor(expression, new SelectList(YesOrNo, "Key", "Value"), htmlAttributes);
        }


        public static IHtmlContent DropCheckBoxForFreightResponsable<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper
           , Expression<Func<TModel, TProperty>> expression
           , object htmlAttributes)
        {
            IDictionary<string, string> YesOrNo = new Dictionary<string, string>
            {
                { "Empresa do Grupo", "Empresa do Grupo" },
                { "Simpress", "Simpress" }
            };

            return htmlHelper.DropDownListFor(expression, new SelectList(YesOrNo, "Key", "Value"), htmlAttributes);
        }


        public static IHtmlContent DropCheckBoxForToner<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper
           , Expression<Func<TModel, TProperty>> expression
           , object htmlAttributes)
        {
            IDictionary<string, string> YesOrNo = new Dictionary<string, string>
            {
                { "Sim", "Sim" },
                { "Não", "Não" }
            };

            return htmlHelper.DropDownListFor(expression, new SelectList(YesOrNo, "Key", "Value"), htmlAttributes);
        }
    }
}
