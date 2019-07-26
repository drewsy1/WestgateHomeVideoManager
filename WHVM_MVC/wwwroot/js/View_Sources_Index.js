// Realtime form validation https://www.sitepoint.com/instant-validation/
// #region Page Variables
const dateTimePickerMinText = $("#date-min-filter-input");
const dateTimePickerMaxText = $("#date-max-filter-input");
const dateTimePickerError = $("#date-filter-error");
const sourceFormatFilterGroup = $("#sourceFormat-tag-filter-group");
const sourceFormatFilterInputs = sourceFormatFilterGroup.find("input");
//#endregion

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
            dateTimePickerError.css("display","none");
        } else if (invalid && !field.attr("aria-invalid")) {
            dateTimePickerMinText.attr("aria-invalid", "true");
            dateTimePickerMaxText.attr("aria-invalid", "true");
            dateTimePickerError.css("display","block");
        }
    }
}

function toggleFilterButtons(filterButtonPressed) {
    let filterButton = $(filterButtonPressed.currentTarget).parent();

    filterButton.siblings(".active").button("toggle");
    filterButton.button("toggle");
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