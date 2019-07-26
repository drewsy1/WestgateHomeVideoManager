using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WHVM_MVC.Helpers
{
    public class JplistFilterHelpers
    {
        public enum JplistTextFilterDataMode
        {
            contains,
            startsWith,
            endsWith,
            equal
        }

        public enum JplistDateFilterDataMode
        {
            dateLower,
            dateUpper
        }

        /// <summary>
        /// Uses TagBuilder's WriteTo method to convert a TagBuilder instance to an HtmlString
        /// </summary>
        /// <param name="tagBuilder">TagBuilder instance to be converted to HtmlString</param>
        /// <returns></returns>
        private static HtmlString ConvertTagBuilderToHtmlString(TagBuilder tagBuilder)
        {
            using (var writer = new StringWriter())
            {
                tagBuilder.WriteTo(writer, HtmlEncoder.Default);
                var htmlOutput = new HtmlString(writer.ToString());
                return htmlOutput;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="classes"></param>
        /// <param name="id"></param>
        /// <param name="attributes"></param>
        /// <param name="childElements"></param>
        /// <returns></returns>
        private static TagBuilder CreateElement(string type, object classes = null, string id = null,
            Dictionary<string, string> attributes = null, object childElements = null)
        {
            TagBuilder newElement = new TagBuilder(type);

            if(!id.IsNullOrEmpty()) newElement.GenerateId(id, ".");

            if (classes is string[] classArray)
            {
                var classesString = string.Join(" ", classArray);
                newElement.AddCssClass(classesString);
            }
            else if (classes is string classString)
            {
                newElement.AddCssClass(classString);
            }

            newElement.MergeAttributes(attributes, true);

            if (childElements is string element)
            {
                newElement.InnerHtml.Append(element);
            }
            else if (childElements is TagBuilder childElementTagBuilder)
            {
                newElement.InnerHtml.AppendHtml(childElementTagBuilder);
            }

            else if (childElements is IList childElementCollection)
            {
                foreach (var childElement in childElementCollection)
                {
                    if (childElement is TagBuilder childElementTagBuilders)
                    {
                        newElement.InnerHtml.AppendHtml(childElementTagBuilders);
                    }
                    else newElement.InnerHtml.Append((string)childElement);
                }
            }

            return newElement;
        }

        /// <summary>
        /// Creates a Jplist-compatible text filter as a TagBuilder object
        /// </summary>
        /// <param name="dataGroup">Corresponds to "data-group" HTML attribute of HTML input element.</param>
        /// <param name="dataName">Corresponds to "data-name" HTML attribute of HTML input element.</param>
        /// <param name="dataPath">Corresponds to "data-path" HTML attribute of HTML input element.</param>
        /// <param name="dataId">Corresponds to "data-id" HTML attribute of HTML input element.</param>
        /// <param name="value">Value of HTML input element.</param>
        /// <param name="id">ID of HTML input element.</param>
        /// <param name="dataMode">Corresponds to "data-mode" HTML attribute of HTML input element.</param>
        /// <param name="cssClasses">Array of strings to be used as CSS classes of HTML input element.</param>
        /// <param name="placeholder">Placeholder text of HTML input element.</param>
        /// <param name="dataClearBtnId">Corresponds to "data-clear-btn-id" HTML attribute of HTML input element.</param>
        /// <param name="type">Type of input element. Defaults to "text"</param>
        /// <returns></returns>
        private static TagBuilder TextboxFilter(
            string dataGroup,
            string dataName,
            string dataPath,
            string dataId,
            string value,
            string id,
            string dataMode,
            string[] cssClasses,
            string placeholder = null,
            string dataClearBtnId = null,
            string type = "text"
        )
        {
            var textBoxFilterAttributes = new Dictionary<String, String>
            {
                {"data-jplist-control", "textbox-filter"},
                {"data-group", dataGroup},
                {"data-name", dataName},
                {"data-path", dataPath},
                {"data-id", dataId},
                {"data-clear-btn-id", dataClearBtnId},
                {"value", value},
                {"data-mode", dataMode},
                {"type", type}
            };
            if (!placeholder.IsNullOrEmpty()) textBoxFilterAttributes.Add("placeholder", placeholder);

            var textBoxFilter = CreateElement("input", cssClasses, id, textBoxFilterAttributes);

            return textBoxFilter;
        }

        /// <summary>
        /// Creates a Jplist-compatible checkbox filter as a TagBuilder object
        /// </summary>
        /// <param name="dataGroup">Corresponds to "data-group" HTML attribute of HTML input element.</param>
        /// <param name="dataName">Corresponds to "data-name" HTML attribute of HTML input element.</param>
        /// <param name="dataPath">Corresponds to "data-path" HTML attribute of HTML input element.</param>
        /// <param name="name">Name of HTML input element.</param>
        /// <param name="value">Value of HTML input element.</param>
        /// <param name="id">ID of HTML input element.</param>
        /// <param name="isChecked">Checked state of HTML input element. Defaults to false</param>
        /// <param name="cssClasses">Array of strings to be used as CSS classes of HTML input element.</param>
        /// <param name="dataClearBtnId">Corresponds to "data-clear-btn-id" HTML attribute</param>
        /// <returns></returns>
        private static TagBuilder CheckboxFilter(
            string dataGroup,
            string dataName,
            string dataPath,
            string name,
            string id,
            object cssClasses,
            bool isChecked = false,
            string value = null,
            string dataClearBtnId = null
        )
        {
            var checkBoxFilterAttributes = new Dictionary<String, String>
            {
                {"data-jplist-control", "checkbox-path-filter"},
                {"data-group", dataGroup},
                {"data-path", dataPath},
                {"data-name", dataName},
                {"data-clear-btn-id", dataClearBtnId},
                {"value", value},
                {"name", name},
                {"type", "checkbox"}
            };
            if (isChecked) checkBoxFilterAttributes.Add("checked", "true");

            var checkBoxFilter = CreateElement("input", cssClasses, id, checkBoxFilterAttributes);

            return checkBoxFilter;
        }

        /// <summary>
        /// Creates a Jplist-compatible radio filter as a TagBuilder object
        /// </summary>
        /// <param name="dataGroup">Corresponds to "data-group" HTML attribute of HTML input element.</param>
        /// <param name="dataName">Corresponds to "data-name" HTML attribute of HTML input element.</param>
        /// <param name="dataPath">Corresponds to "data-path" HTML attribute of HTML input element.</param>
        /// <param name="name">Name of HTML input element.</param>
        /// <param name="value">Value of HTML input element.</param>
        /// <param name="id">ID of HTML input element.</param>
        /// <param name="isChecked">Checked state of HTML input element. Defaults to false</param>
        /// <param name="cssClasses">Array of strings to be used as CSS classes of HTML input element.</param>
        /// <returns></returns>
        private static TagBuilder RadioFilter(
            string dataGroup,
            string dataName,
            string dataPath,
            string name,
            string id,
            object cssClasses,
            bool isChecked = false,
            string value = null
        )
        {
            var radioFilterAttributes = new Dictionary<String, String>
            {
                {"data-jplist-control", "radio-buttons-path-filter"},
                {"data-group", dataGroup},
                {"data-path", dataPath},
                {"data-name", dataName},
                {"value", value},
                {"name", name},
                {"type", "radio"}
            };
            if (isChecked) radioFilterAttributes.Add("checked", "true");

            var radioFilter = CreateElement("input", cssClasses, id, radioFilterAttributes);

            return radioFilter;
        }

        /// <summary>
        /// Generates a custom-styled Jplist text filter.
        /// </summary>
        /// <param name="elementGroupId">ID of the outermost div element.</param>
        /// <param name="elementDataGroup">Corresponds to "data-group" HTML attribute of HTML input element</param>
        /// <param name="elementDataName">Corresponds to "data-name" HTML attribute of HTML input element</param>
        /// <param name="elementDataPath">Corresponds to "data-path" HTML attribute of HTML input element</param>
        /// <param name="elementDataId">Corresponds to "data-id" HTML attribute of HTML input element</param>
        /// <param name="elementValue">Value of HTML input element</param>
        /// <param name="elementId">ID of HTML input element</param>
        /// <param name="elementDataMode">Corresponds to "data-mode" HTML attribute of HTML input element</param>
        /// <param name="elementPlaceholder">Placeholder text for HTML input element. Defaults to null.</param>
        /// <param name="elementDataClearBtnId">Corresponds to "data-clear-btn-id" HTML attribute of HTML input element. Defaults to null.</param>
        /// <returns></returns>
        public static HtmlString WHVMTextFilter(
            string elementGroupId,
            string elementDataGroup,
            string elementDataName,
            string elementDataPath,
            string elementDataId,
            string elementValue,
            string elementId,
            JplistTextFilterDataMode elementDataMode = JplistTextFilterDataMode.contains,
            string elementPlaceholder = null,
            string elementDataClearBtnId = null
        )
        {
            TagBuilder inputGroupDiv;

            if (elementDataClearBtnId.IsNullOrEmpty())
            {
                inputGroupDiv = CreateElement("div", "input-group-append");
            }
            else
            {
                var inputGroupClearBtn = CreateElement("button", new[] {"btn", "btn-outline-secondary"},
                    elementDataClearBtnId, new Dictionary<String, String> {{"type", "button"}}, "Clear");

                inputGroupDiv = CreateElement("div", "input-group-append",childElements:inputGroupClearBtn);
            }

            var inputTextBox = TextboxFilter(elementDataGroup,
                elementDataName,
                elementDataPath,
                elementDataId,
                elementValue,
                elementId,
                elementDataMode.ToString(),
                new[] {"form-control", "form-control-sm"},
                elementPlaceholder,
                elementDataClearBtnId);

            var outerDiv = CreateElement("div", new[] {"input-group", "input-group-sm", "py-1", "py-lg-2"},
                elementGroupId, childElements: new ArrayList {inputTextBox, inputGroupDiv});

            return ConvertTagBuilderToHtmlString(outerDiv);
        }

        /// <summary>
        /// Generates a custom-styled Jplist date filter.
        /// </summary>
        /// <param name="elementGroupId">ID of the outermost div element.</param>
        /// <param name="elementGroupText">Inner text of the outermost div element.</param>
        /// <param name="elementDataGroup">Corresponds to "data-group" HTML attribute of HTML input element.</param>
        /// <param name="elementDataName">Corresponds to "data-name" HTML attribute of HTML input element.</param>
        /// <param name="elementDataPath">Corresponds to "data-path" HTML attribute of HTML input element.</param>
        /// <param name="elementDataId">Corresponds to "data-id" HTML attribute of HTML input element.</param>
        /// <param name="elementValue">Value of HTML input element.</param>
        /// <param name="elementId">ID of HTML input element.</param>
        /// <param name="elementDataMode">Corresponds to "data-mode" HTML attribute of HTML input element.</param>
        /// <returns></returns>
        public static HtmlString WHVMDateFilter(
            string elementGroupId,
            string elementGroupText,
            string elementDataGroup,
            string elementDataName,
            string elementDataPath,
            string elementDataId,
            string elementValue,
            string elementId,
            JplistDateFilterDataMode elementDataMode
        )
        {
            var inputDateBox = TextboxFilter(elementDataGroup,
                elementDataName,
                elementDataPath,
                elementDataId,
                elementValue,
                elementId,
                elementDataMode.ToString(),
                new[] {"form-control", "form-control-sm"},
                null,
                null,
                "date");

            var innerGroupTextSpan = CreateElement("span", "input-group-text", childElements: elementGroupText);
            var inputGroupDiv = CreateElement("div", "input-group-prepend", childElements: innerGroupTextSpan);
            var outerDiv = CreateElement("div", new[] {"input-group", "input-group-sm", "py-1", "py-lg-2"},
                elementGroupId, childElements: new ArrayList {inputGroupDiv, inputDateBox});

            return ConvertTagBuilderToHtmlString(outerDiv);
        }

        /// <summary>
        /// Generates a custom-styled sidebar accordion card header.
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="collapseId"></param>
        /// <param name="clearButtonId"></param>
        /// <param name="headerText"></param>
        /// <returns></returns>
        public static HtmlString WHVMSidebarAccordionCardHeader(
            string cardId,
            string collapseId,
            string clearButtonId,
            string headerText)
        {
            var cardHeaderButtonIconClasses = new[] { "mi", "mi-ChevronRight" };
            var cardHeaderButtonIcon = CreateElement("i", cardHeaderButtonIconClasses);

            var cardHeaderButtonText = CreateElement("span", "mx-1", childElements: headerText);

            var cardHeaderButtonChildren = new ArrayList { cardHeaderButtonIcon, cardHeaderButtonText };
            var cardHeaderButtonClasses = new[] { "btn", "btn-link", "collapsed", "px-0", "d-flex" };
            var cardHeaderButtonAttributes = new Dictionary<string, string>
            {
                {"data-toggle", "collapse"},
                {"data-target", "#" + collapseId},
                {"aria-expanded", "false"},
                {"aria-controls", collapseId}
            };
            var cardHeaderButton = CreateElement("button", cardHeaderButtonClasses, null, cardHeaderButtonAttributes, cardHeaderButtonChildren);

            var cardHeaderBadgeClasses = new[]{"p-1","flex-grow-0","badge","badge-primary","badge-pill","d-none","text-truncate","mx-1"};
            var cardHeaderBadgeAttributes = new Dictionary<string, string> {{"data-toggle", "tooltip"}};
            var cardHeaderBadge = CreateElement("div", cardHeaderBadgeClasses,
                attributes: cardHeaderBadgeAttributes);

            var cardHeaderClearIconClasses = new[] { "mi", "mi-Clear" };
            var cardHeaderClearIcon = CreateElement("i", cardHeaderClearIconClasses);

            var cardHeaderClearAClasses = new[] { "btn","btn-sm","btn-link","disabled","p-0" };
            var cardHeaderClearAAttributes = new Dictionary<string, string>
            {
                {"href","#" },
                {"role", "button"}
            };
            var cardHeaderClearA = CreateElement("a", cardHeaderClearAClasses, clearButtonId,
                cardHeaderClearAAttributes, cardHeaderClearIcon);

            var cardHeaderClear = CreateElement("div", "ml-auto", childElements: cardHeaderClearA);

            var cardHeaderClasses = new[] { "card-header", "d-flex", "align-items-center"};
            var cardHeaderChildren = new ArrayList { cardHeaderButton, cardHeaderBadge, cardHeaderClear };
            var cardHeader = CreateElement("div", cardHeaderClasses, cardId + "-header",
                childElements: cardHeaderChildren);

            return ConvertTagBuilderToHtmlString(cardHeader);
        }

        /// <summary>
        /// Generates a custom-styled sidebar accordion card radio list item
        /// </summary>
        /// <param name="dataGroup"></param>
        /// <param name="dataName"></param>
        /// <param name="dataPath"></param>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="isChecked"></param>
        /// <param name="labelText"></param>
        /// <returns></returns>
        private static TagBuilder WHVMSidebarAccordionCardBodyListRadioItem(
            string dataGroup,
            string dataName,
            string dataPath,
            string name,
            string id,
            bool isChecked,
            string labelText)
        {
            var radioClasses = new[] {"custom-control-input"};
            if (isChecked) radioClasses.Append("jplist-selected");
            var radio = RadioFilter(dataGroup, dataName, dataPath, name, id, radioClasses, isChecked);

            var radioLabelAttributes = new Dictionary<string, string> {{"for",id }};
            var radioLabel = CreateElement("label", "custom-control-label", null, radioLabelAttributes, labelText);

            var radioLiChildren = new ArrayList { radio,radioLabel };
            var radioLiClasses = new[] { "my-1", "custom-control", "custom-radio" };
            var radioLi = CreateElement("li", radioLiClasses, childElements: radioLiChildren);

            return radioLi;
        }

        /// <summary>
        /// Generates a custom-styled sidebar accordion card radio list item
        /// </summary>
        /// <param name="dataGroup"></param>
        /// <param name="dataName"></param>
        /// <param name="dataPath"></param>
        /// <param name="dataClearBtnId"></param>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="isChecked"></param>
        /// <param name="labelText"></param>
        /// <returns></returns>
        private static TagBuilder WHVMSidebarAccordionCardBodyListCheckboxItem(
            string dataGroup,
            string dataName,
            string dataPath,
            string dataClearBtnId,
            string name,
            string id,
            bool isChecked,
            string labelText)
        {
            var checkboxClasses = new[] { "custom-control-input" };
            if (isChecked) checkboxClasses.Append("jplist-selected");
            var checkbox = CheckboxFilter(dataGroup, dataName, dataPath, name, id, checkboxClasses, isChecked,dataClearBtnId: dataClearBtnId);

            var checkboxLabelAttributes = new Dictionary<string, string> { { "for", id } };
            var checkboxLabel = CreateElement("label", "custom-control-label", null, checkboxLabelAttributes, labelText);

            var checkboxLiChildren = new ArrayList { checkbox, checkboxLabel };
            var checkboxLiClasses = new[] { "my-1", "custom-control", "custom-checkbox" };
            var checkboxLi = CreateElement("li", checkboxLiClasses, childElements: checkboxLiChildren);

            return checkboxLi;
        }

        /// <summary>
        /// Generates a custom-styled sidebar accordion card radio list
        /// </summary>
        /// <param name="dataGroup"></param>
        /// <param name="dataName"></param>
        /// <param name="filterProperty"></param>
        /// <param name="valueProperty"></param>
        /// <param name="idTemplate"></param>
        /// <param name="objectCollection"></param>
        /// <returns></returns>
        public static TagBuilder WHVMSidebarAccordionCardBodyListRadio(
            string dataGroup,
            string dataName,
            string filterProperty,
            string valueProperty,
            string idTemplate,
            IEnumerable<object> objectCollection)
        {
            var defaultRadio = WHVMSidebarAccordionCardBodyListRadioItem(dataGroup, dataName, "default", dataName,
                idTemplate + "default", true, "Any");

            var radioListAttributes = new Dictionary<string, string> {{"style", "list-style-type: none; margin: 0; margin-left: 1rem; padding: 0"}};
            var radioListChildren = new ArrayList {defaultRadio};

            foreach (var currentObject in objectCollection)
            {
                Type objType = currentObject.GetType();
                PropertyInfo[] objProperties = objType.GetProperties();
                PropertyInfo filterPropertyObj = objProperties.First(prop => prop.Name == filterProperty);
                PropertyInfo valuePropertyObj = objProperties.First(prop => prop.Name == valueProperty);
                var filterPropertyString = filterPropertyObj.GetValue(currentObject);
                var valuePropertyString = valuePropertyObj.GetValue(currentObject);

                string currentObjectId = idTemplate + filterPropertyString;

                var tempRadio = WHVMSidebarAccordionCardBodyListRadioItem(dataGroup, dataName, "." + currentObjectId,
                    dataName, currentObjectId, false, (string)valuePropertyString);

                radioListChildren.Add(tempRadio);
            }

            var radioList = CreateElement("ul", attributes: radioListAttributes, childElements: radioListChildren);

            return radioList;
        }

        /// <summary>
        /// Generates a custom-styled sidebar accordion card radio list
        /// </summary>
        /// <param name="dataGroup"></param>
        /// <param name="dataNameTemplate"></param>
        /// <param name="dataClearBtnId"></param>
        /// <param name="filterProperty"></param>
        /// <param name="valueProperty"></param>
        /// <param name="idTemplate"></param>
        /// <param name="objectCollection"></param>
        /// <returns></returns>
        public static TagBuilder WHVMSidebarAccordionCardBodyListCheckbox(
            string dataGroup,
            string dataNameTemplate,
            string dataClearBtnId,
            string filterProperty,
            string valueProperty,
            string idTemplate,
            IEnumerable<object> objectCollection)
        {
            var checkboxListAttributes = new Dictionary<string, string> {{"style", "list-style-type: none; margin: 0; margin-left: 1rem; padding: 0"}};
            var checkboxListChildren = new ArrayList();

            foreach (var currentObject in objectCollection)
            {
                Type objType = currentObject.GetType();
                PropertyInfo[] objProperties = objType.GetProperties();
                PropertyInfo filterPropertyObj = objProperties.First(prop => prop.Name == filterProperty);
                PropertyInfo valuePropertyObj = objProperties.First(prop => prop.Name == valueProperty);
                var filterPropertyString = filterPropertyObj.GetValue(currentObject);
                var valuePropertyString = valuePropertyObj.GetValue(currentObject);

                string currentObjectId = idTemplate + filterPropertyString;
                string currentObjectName = dataNameTemplate + filterPropertyString;

                var tempCheckbox = WHVMSidebarAccordionCardBodyListCheckboxItem(dataGroup, currentObjectName, "." + currentObjectId, dataClearBtnId,
                    currentObjectName, currentObjectId, false, (string)valuePropertyString);

                checkboxListChildren.Add(tempCheckbox);
            }

            var checkboxList = CreateElement("ul", attributes: checkboxListAttributes, childElements: checkboxListChildren);

            return checkboxList;
        }

        /// <summary>
        /// Generates a custom-styled sidebar accordion card body
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="collapseId"></param>
        /// <param name="accordionId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static HtmlString WHVMSidebarAccordionCardBody(
            string cardId,
            string collapseId,
            string accordionId,
            TagBuilder content)
        {
            var cardBodyContent = CreateElement("div", "card-body", childElements: content);

            var cardBodyAttributes = new Dictionary<string, string>
            {
                {"aria-labelledby", cardId + "-header"},
                {"data-parent", "#" + accordionId}
            };
            var cardBody = CreateElement("div", "collapse", collapseId, cardBodyAttributes, cardBodyContent);

            return ConvertTagBuilderToHtmlString(cardBody);
        }
    }
}