/* global document,jplist */

//#region Constant Variables
const clipsPeopleFilterElement = $('#clips-persons-filter');
const clipsPeopleReviewerFilterElement = $('#clips-persons-reviewer-filter');
const clipsPeopleCameraFilterElement = $('#clips-persons-camera-filter');
const clipsCollectionsFilterElement = $('#clips-collections-filter');
const clipsSourceFilterElement = $('#clips-source-filter');
const clipsPeopleFilter = clipsPeopleFilterElement.find('input');
const clipsPeopleReviewerFilter = clipsPeopleReviewerFilterElement.find('input');
const clipsPeopleCameraFilter = clipsPeopleCameraFilterElement.find('input');
const clipsCollectionsFilter = clipsCollectionsFilterElement.find('input');
const clipsSourceFilter = clipsSourceFilterElement.find('input');
const filterAccordionObjects = $('#filter-accordion');
const filterAccordionButtonObjects = filterAccordionObjects.find('.collapse');
const CheckboxCollections = [
    {
        element: clipsPeopleFilterElement,
        collection: clipsPeopleFilter,
        color: "badge-blue"
    },
    {
        element: clipsCollectionsFilterElement,
        collection: clipsCollectionsFilter,
        color: "badge-blue"
    }
    ];
const RadioCollections= [
    {
        element: clipsPeopleReviewerFilterElement,
        collection: clipsPeopleReviewerFilter,
        color: "badge-blue"
    },
    {
        element: clipsPeopleCameraFilterElement,
        collection: clipsPeopleCameraFilter,
        color: "badge-blue"
    },
    {
        element: clipsSourceFilterElement,
        collection: clipsSourceFilter,
        color: "badge-blue"
    }
];

//#endregion

//#region Page Functions
/**
 *
 * @param checkboxFilters
 */
function refreshCheckboxFilterHighlights() {
    let itemId;
    let itemElement;

    for (const currentCheckboxFilter of CheckboxCollections) {
        let countTextElement = currentCheckboxFilter.element.find(".badge.badge-primary");
        let clearButton = currentCheckboxFilter.element.find("a");
        let checkboxesSelected = currentCheckboxFilter.element.find("input:checked");
        let checkboxesSelectedCount = checkboxesSelected.length;

        countTextElement.text(checkboxesSelectedCount);
        countTextElement.toggleClass("d-none", !(checkboxesSelectedCount > 0));
        clearButton.toggleClass("disabled",!(checkboxesSelectedCount > 0));

        for (const arrayItem of currentCheckboxFilter.collection) {
            itemId = $(arrayItem).attr("data-path")
            itemElement = $(itemId);

            itemElement.toggleClass(currentCheckboxFilter.color, arrayItem.checked)
            itemElement.toggleClass("badge-secondary", !arrayItem.checked)
        }
    }
    for (const currentRadioFilter of RadioCollections) {
        let countTextElement = currentRadioFilter.element.find(".badge.badge-primary");
        let clearButton = currentRadioFilter.element.find("a");
        let radioSelected = currentRadioFilter.element.find(".jplist-selected");
        let radioIsDefault = $(radioSelected).attr('data-path') === 'default'
        let radioSelectedText = $(radioSelected).val();

        countTextElement.text(radioSelectedText);
        countTextElement.attr("title",radioSelectedText);
        countTextElement.toggleClass("d-none", radioIsDefault);
        clearButton.toggleClass("disabled", radioIsDefault);

        for (const arrayItem of currentRadioFilter.collection) {
            itemId = $(arrayItem).attr("data-path")
            itemElement = $(itemId);

            itemElement.toggleClass(currentRadioFilter.color, arrayItem.checked)
            itemElement.toggleClass("badge-secondary", !arrayItem.checked)
        }
    }
}

function radioFilterClear(dataNameAttr){
    const radioButtons = Array.from(document.querySelectorAll('[data-name='+dataNameAttr+']'));
    const radioButtonDefault = document.querySelectorAll('[data-path="default"][data-name='+dataNameAttr+']')
    // radioButtons.prop("checked",false).toggleClass("jplist-selected",false)
    // radioButtonDefault.prop("checked",true).toggleClass("jplist-selected",true)
    radioButtons.forEach(function(Element){jplist.resetControl(Element)})
    jplist.resetControl(radioButtonDefault);
}
//#endregion

//#region Event Listeners
document.getElementById('text-name-filter-input').addEventListener('jplist.state', function () {
    refreshCheckboxFilterHighlights();
});

$(filterAccordionButtonObjects).on('show.bs.collapse', function () {
    let thisIcon = $(this).parent().find('i');
    thisIcon.toggleClass("mi-ChevronDown", true)
    thisIcon.toggleClass("mi-ChevronRight", false)
});

$(filterAccordionButtonObjects).on('hide.bs.collapse', function () {
    let thisIcon = $(this).parent().find('i');
    thisIcon.toggleClass("mi-ChevronDown", false)
    thisIcon.toggleClass("mi-ChevronRight", true)
});

$("#clips-persons-reviewer-filter-clear").click(function(){
    radioFilterClear('radio-person-reviewer');
})

$("#clips-persons-camera-filter-clear").click(function(){
    radioFilterClear('radio-person-camera');
})

$("#clips-source-filter-clear").click(function(){
    radioFilterClear('radio-source');
})

$(document).ready(function(){
    refreshCheckboxFilterHighlights();
});
//#endregion

