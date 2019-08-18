/* global ModelsList */
const modelSelectorCard = $('#modelSelector');
const modelSelectorCollapse = modelSelectorCard.find('.collapse');

$(modelSelectorCollapse).on('show.bs.collapse', function () {
    let bottomIcon = $(this).parent().find('#modelSelectorHeaderBottom').find('i');
    let topIcon = $(this).parent().find('#modelSelectorHeaderTop').find('i');

    bottomIcon.toggleClass("mi-ChevronUp", true)
    bottomIcon.toggleClass("mi-ChevronRight", false)

    topIcon.toggleClass("mi-ChevronDown", true)
    topIcon.toggleClass("mi-ChevronRight", false)
});

$(modelSelectorCollapse).on('hide.bs.collapse', function () {
    let bottomIcon = $(this).parent().find('#modelSelectorHeaderBottom').find('i');
    let topIcon = $(this).parent().find('#modelSelectorHeaderTop').find('i');

    bottomIcon.toggleClass("mi-ChevronUp", false)
    bottomIcon.toggleClass("mi-ChevronRight", true)

    
    topIcon.toggleClass("mi-ChevronDown", false)
    topIcon.toggleClass("mi-ChevronRight", true)
});