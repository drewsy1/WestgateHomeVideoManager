// Realtime form validation https://www.sitepoint.com/instant-validation/
//#region Page Variables
const dateTimePickerMinText = $('#dateTimePickerMinText');
const dateTimePickerMaxText = $('#dateTimePickerMaxText');
const dateTimePickerError = $('#dateTimePickerError');
const sourceFormatFilterGroup = $('#sourceFormat-tag-filter-group');
const sourceFormatFilterInputs = sourceFormatFilterGroup.find('input');
const sourceFormatFilterLabels = sourceFormatFilterGroup.find('label');
//#endregion

//#region Page Functions
/**
 * Adds a callback function to an element's event
 * @param node - HTML element to which the eventListener should be attached
 * @param type - Event to be attached
 * @param callback - Function to be attached to the event
 */
function addEvent(node, type, callback) {
    if (node.addEventListener) {
        node.addEventListener(type, function (e) {
            callback(e, e.target);
        }, false);
    } else if (node.attachEvent) {
        node.attachEvent('on' + type, function (e) {
            callback(e, e.srcElement);
        });
    }
}

/**
 * Confirms that a field should be validated
 * @param field - HTML input element to be tested
 * @returns {boolean}
 */
function shouldBeValidated(field) {
    return (
        !(field.attr("readonly") || field.readonly) &&
        !(field.attr("disabled") || field.disabled)
    );
}

/**
 * Sets an input field's aria-invalid attribute and changes its appearance if the field is invalid.
 * @param field - HTML input element to be modified
 */
function instantValidation(field) {
    if (shouldBeValidated(field)) {
        let invalid =
            (dateTimePickerMinText.val().length > 0) &&
            (dateTimePickerMaxText.val().length > 0) &&
            (new Date(dateTimePickerMaxText.val()) < new Date(dateTimePickerMinText.val()));
        if (!invalid && field.attr("aria-invalid")) {
            dateTimePickerMinText.removeAttr("aria-invalid");
            dateTimePickerMaxText.removeAttr("aria-invalid");
            dateTimePickerError.css('display','none');
        } else if (invalid && !field.attr("aria-invalid")) {
            dateTimePickerMinText.attr("aria-invalid", "true");
            dateTimePickerMaxText.attr("aria-invalid", "true");
            dateTimePickerError.css('display','block');
        }
    }
}

function toggleFilterButtons(filterButtonPressed) {
    let filterButton = $(filterButtonPressed.currentTarget).parent();

    filterButton.siblings('.active').button('toggle');
    filterButton.button('toggle');
}
//#endregion

//#region Page Event Listeners
dateTimePickerMinText.change(function (e) {
    instantValidation($(e.currentTarget));
});

dateTimePickerMaxText.change(function (e) {
    instantValidation($(e.currentTarget));
});

sourceFormatFilterInputs.change(
    toggleFilterButtons
);//#endregion