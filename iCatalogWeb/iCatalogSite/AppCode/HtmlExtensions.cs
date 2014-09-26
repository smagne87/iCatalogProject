using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc.Html
{
    public static class HtmlExtensions
    {
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString LabelForRequired<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText = "")
        {
            return LabelHelper(html, ModelMetadata.FromLambdaExpression(expression, html.ViewData), ExpressionHelper.GetExpressionText(expression), labelText);
        }

        public static string Label(this HtmlHelper helper, string target, string text)
        {
            return String.Format("<label for='{0}'>{1}</label>", target, text);
        }

        private static MvcHtmlString LabelHelper(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, string labelText)
        {
            bool isRequired = (metadata.IsRequired && metadata.ModelType != typeof(System.Boolean));

            if (string.IsNullOrEmpty(labelText))
            {
                labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            }

            if (string.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }

            TagBuilder tag = new TagBuilder("label");

            tag.Attributes.Add("for", TagBuilder.CreateSanitizedId(html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)));

            tag.Attributes.Add("class", "control-label");
            if (isRequired)
            {
                tag.Attributes["class"] = "control-label label-required";
            }
            tag.InnerHtml = labelText;
            if (isRequired)
            {
                var asteriskTag = new TagBuilder("span");
                asteriskTag.Attributes.Add("class", "required");
                asteriskTag.SetInnerText("*");
                tag.InnerHtml = asteriskTag.ToString(TagRenderMode.Normal) + labelText;
            }

            var output = tag.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(output);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString CustomCheckBox<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var fieldName = ExpressionHelper.GetExpressionText(expression);

            string displayName = metadata.DisplayName ?? metadata.PropertyName ?? fieldName.Split('.').Last();

            if (!string.IsNullOrEmpty(fieldName))
            {
                string output = "";
                if (metadata.Model != null)
                    output = CustomCheckBoxHelper(html, fieldName, htmlAttributes, (bool)metadata.Model);
                else
                    output = CustomCheckBoxHelper(html, fieldName, htmlAttributes);

                return MvcHtmlString.Create(output);
            }
            return MvcHtmlString.Empty;
        }

        public static string CustomCheckBoxHelper(this HtmlHelper helper, string fieldName, object htmlAttributes, bool value = false)
        {
            var tagBuilder = new TagBuilder("input");

            tagBuilder.MergeAttribute("type", "checkbox");
            tagBuilder.MergeAttribute("name", fieldName);

            tagBuilder.GenerateId(fieldName);

            tagBuilder.MergeAttribute("value", "true");

            if (value)
            {
                tagBuilder.MergeAttribute("checked", "checked");
            }

            if (htmlAttributes != null)
                tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), replaceExisting: true);

            return tagBuilder.ToString(TagRenderMode.Normal);
        }
    }
}