//#region Constant Variables
const clipsPeopleFilterElement = document.getElementById("clipsPeopleFilter");
const clipsCollectionsFilterElement = document.getElementById("clipsCollectionsFilter");
const clipsPeopleFilter = Array.from(clipsPeopleFilterElement.getElementsByTagName("input"));
const clipsCollectionsFilter = Array.from(clipsCollectionsFilterElement.getElementsByTagName("input"));
const filterAccordionObjects = document.getElementById("FilterAccordion");
const filterAccordionButtonObjects = Array.from(filterAccordionObjects.getElementsByTagName("a"));
const iconChevronToggleObject = {true: "mi-ChevronDown", false: "mi-ChevronRight"};
const CheckboxCollections = [{element: clipsPeopleFilterElement, collection: clipsPeopleFilter, color:"badge-purple"},{element: clipsCollectionsFilterElement, collection: clipsCollectionsFilter, color:"badge-teal"}];
//#endregion

//#region Page Functions
/**
 * Replaces an HTML element's class with another
 * @param htmlElement - HTML element to be modified
 * @param {string}oldClass - Class to be replaced
 * @param {string}newClass - Class to be added
 */
function replaceClass(htmlElement, oldClass, newClass) {
    htmlElement.classList.remove(oldClass);
    htmlElement.classList.add(newClass);
}

/**
 *
 * @param htmlElement
 * @param highlight
 */
function tagHighlightOn(htmlElement, highlight) {
    replaceClass(htmlElement, "badge-secondary", highlight);
}

/**
 *
 * @param htmlElement
 * @param highlight
 */
function tagHighlightOff(htmlElement, highlight) {
    replaceClass(htmlElement, highlight, "badge-secondary");
}

/**
 *
 * @param checkboxFilters
 */
function refreshCheckboxFilterHighlights(checkboxFilters) {
    let itemId;
    let itemArray;

    for (const currentCheckboxFilter of checkboxFilters) {
        let countTextElement = currentCheckboxFilter.element.getElementsByClassName("badge badge-primary")[0];
        let clearButton = currentCheckboxFilter.element.getElementsByTagName("a")[0];
        let checkboxesSelected = Array.from(currentCheckboxFilter.element.getElementsByClassName("jplist-selected"));
        let checkboxesSelectedCount = checkboxesSelected.length;

        if(checkboxesSelectedCount > 0){
            countTextElement.innerText = checkboxesSelectedCount;
            countTextElement.classList.remove("d-none");
            clearButton.classList.remove("disabled");
        }
        else{
            countTextElement.innerText = "";
            countTextElement.classList.add("d-none");
            clearButton.classList.add("disabled");
        }

        for (const arrayItem of currentCheckboxFilter.collection) {
            itemId = arrayItem.attributes.getNamedItem("id").value.replace("-cb", "");
            itemArray = Array.from(document.getElementsByClassName(itemId));

            if (arrayItem.checked) {
                for (const element of itemArray) {
                    tagHighlightOn(element, currentCheckboxFilter.color);
                }
            } else {
                for (const element of itemArray) {
                    tagHighlightOff(element, currentCheckboxFilter.color);
                }
            }
        }
    }
}

/**
 * Changes the class of a target's child icon(s) based on the boolean value of a specified attribute.
 * @param target - HTML object whose child icon(s) should be affected
 * @param {object} iconObject - object containing a key for each boolean value and the desired HTML class for each
 * @param {string} targetAttribute - desired boolean attribute to be tested
 */
function toggleIcon(target, iconObject, targetAttribute) {
            const item = target.getElementsByTagName("i")[0];
            const ariaExpanded = target.getAttribute(targetAttribute) === "true";
            replaceClass(item, iconObject[ariaExpanded], iconObject[!ariaExpanded]);
}
//#endregion

//#region Event Listeners
document.getElementById('textFilterInput').addEventListener('jplist.state', function (e) {
    refreshCheckboxFilterHighlights(CheckboxCollections);
});

for (const button of filterAccordionButtonObjects) {
    button.addEventListener("click", function (e) {
        toggleIcon(e.currentTarget, iconChevronToggleObject, "aria-expanded");
    });
}
//#endregion