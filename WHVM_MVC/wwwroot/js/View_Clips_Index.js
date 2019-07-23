/* global document,jplist */

//#region Constant Variables
const clipsPeopleFilterElement = $('#clipsPeopleFilter');
const clipsPeopleReviewerFilterElement = $('#clipsPeopleReviewerFilter');
const clipsPeopleCameraFilterElement = $('#clipsPeopleCameraFilter');
const clipsCollectionsFilterElement = $('#clipsCollectionsFilter');
const clipsSourceFilterElement = $('#clipsSourceFilter');
const clipsPeopleFilter = clipsPeopleFilterElement.find('input');
const clipsPeopleReviewerFilter = clipsPeopleReviewerFilterElement.find('input');
const clipsPeopleCameraFilter = clipsPeopleCameraFilterElement.find('input');
const clipsCollectionsFilter = clipsCollectionsFilterElement.find('input');
const clipsSourceFilter = clipsSourceFilterElement.find('input');
const filterAccordionObjects = $('#FilterAccordion');
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
document.getElementById('textFilterInput').addEventListener('jplist.state', function () {
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

$("#clipsPeopleReviewerFilterClear").click(function(){
    radioFilterClear('radio-person-reviewer');
})

$("#clipsPeopleCameraFilterClear").click(function(){
    radioFilterClear('radio-person-camera');
})

$("#clipsSourceFilterClear").click(function(){
    radioFilterClear('radio-source');
})

$(document).ready(function(){
    refreshCheckboxFilterHighlights();
});
//#endregion

