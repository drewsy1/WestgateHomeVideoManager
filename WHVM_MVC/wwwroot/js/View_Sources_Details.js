/* global ModelsList */
const modelSelectorCard = $('#modelSelector');
const modelSelectorCollapse = modelSelectorCard.find('.collapse');

console.log(ModelsList);

$(modelSelectorCollapse).on('show.bs.collapse', function () {
    let thisIcon = $(this).parent().find('i');
    thisIcon.toggleClass("mi-ChevronUp", true)
    thisIcon.toggleClass("mi-ChevronRight", false)
});

$(modelSelectorCollapse).on('hide.bs.collapse', function () {
    let thisIcon = $(this).parent().find('i');
    thisIcon.toggleClass("mi-ChevronUp", false)
    thisIcon.toggleClass("mi-ChevronRight", true)
});