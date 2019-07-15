//#region Constant Variables
const clipsPeopleFilterElement = $('#clipsPeopleFilter');
const clipsCollectionsFilterElement = $('#clipsCollectionsFilter');
const clipsPeopleFilter = $('#clipsPeopleFilter > input');
const clipsCollectionsFilter = $('#clipsCollectionsFilter > input');
const filterAccordionObjects = $('#FilterAccordion');
const filterAccordionButtonObjects = filterAccordionObjects.find('button');
const iconChevronToggleObject = {true: "mi-ChevronDown", false: "mi-ChevronRight"};
const CheckboxCollections = [
    {
        element: clipsPeopleFilterElement,
        collection: clipsPeopleFilter,
        color: "badge-purple"
    },
    {
        element: clipsCollectionsFilterElement,
        collection: clipsCollectionsFilter,
        color: "badge-teal"
    }];
//#endregion

//#region Page Functions
/**
 *
 * @param checkboxFilters
 */
function refreshCheckboxFilterHighlights(checkboxFilters) {
    let itemId;
    let itemArray;

    for (const currentCheckboxFilter of checkboxFilters) {
        let countTextElement = currentCheckboxFilter.element.find(".badge.badge-primary");
        let clearButton = currentCheckboxFilter.element.find("a");
        let checkboxesSelected = currentCheckboxFilter.element.find(".jplist-selected");
        let checkboxesSelectedCount = checkboxesSelected.length;

        countTextElement.text(checkboxesSelectedCount);
        countTextElement.toggleClass("d-none", !(checkboxesSelectedCount > 0));
        clearButton.toggleClass("disabled",!(checkboxesSelectedCount > 0));

        for (const arrayItem of currentCheckboxFilter.collection) {
            itemID = arrayItem.attr("id",function(i,val){
                return val.replace('-cb','')
            })
            itemArray = $('#'+itemId);

            itemArray.toggleClass(currentCheckboxFilter.color, arrayItem.checked)
            itemArray.toggleClass("badge-secondary", !arrayItem.checked)
        }
    }
}
//#endregion

//#region Event Listeners
$('#textFilterInput').on('jplist.state', function (e) {
    refreshCheckboxFilterHighlights(CheckboxCollections);
});

filterAccordionButtonObjects.click(function () {
    let thisIcon = $(this).find('i');
    let ariaExpanded = $(this).attr("aria-expanded") === 'true'
    thisIcon.toggleClass("mi-ChevronDown", !ariaExpanded)
    thisIcon.toggleClass("mi-ChevronRight", ariaExpanded)
});
//#endregion