// Realtime form validation https://www.sitepoint.com/instant-validation/
//#region Page Variables
const dateTimePickerMinText = document.getElementById("dateTimePickerMinText");
const dateTimePickerMaxText = document.getElementById("dateTimePickerMaxText");
const dateTimePickerError = document.getElementById("dateTimePickerError");
const sourceFormatFilterGroup = document.getElementById("sourceFormat-tag-filter-group");
const sourceFormatFilterInputs = sourceFormatFilterGroup.getElementsByTagName("input");
const sourceFormatFilterInputsArray = Array.from(sourceFormatFilterInputs);
const sourceFormatFilterLabels = sourceFormatFilterGroup.getElementsByTagName("label");
const sourceFormatFilterLabelsArray = Array.from(sourceFormatFilterLabels);
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
        !(field.getAttribute("readonly") || field.readonly) &&
        !(field.getAttribute("disabled") || field.disabled)
    );
}

/**
 * Sets an input field's aria-invalid attribute and changes its appearance if the field is invalid.
 * @param field - HTML input element to be modified
 */
function instantValidation(field) {
    if (shouldBeValidated(field)) {
        let invalid =
            (dateTimePickerMinText.value.length > 0) &&
            (dateTimePickerMaxText.value.length > 0) &&
            (new Date(dateTimePickerMaxText.value) < new Date(dateTimePickerMinText.value));
        if (!invalid && field.getAttribute("aria-invalid")) {
            dateTimePickerMinText.removeAttribute("aria-invalid");
            dateTimePickerMaxText.removeAttribute("aria-invalid");
            dateTimePickerError.style.display = "none";
        } else if (invalid && !field.getAttribute("aria-invalid")) {
            dateTimePickerMinText.setAttribute("aria-invalid", "true");
            dateTimePickerMaxText.setAttribute("aria-invalid", "true");
            dateTimePickerError.style.display = "block";
        }
    }
}

function toggleFilterButtons(filterButtonPressed, filterButtonsArray) {
    for (let filterButton of filterButtonsArray) {
        filterButton.classList.remove("active");
        filterButton.setAttribute("aria-pressed","false");
    }

    let filterButtonLabel = filterButtonPressed.parentElement;

    filterButtonLabel.classList.add("active");
    filterButtonPressed.setAttribute("aria-pressed", "true");
}
//#endregion

//#region Page Event Listeners
dateTimePickerMinText.addEventListener("change", function (e, target) {
    instantValidation(target);
});

dateTimePickerMaxText.addEventListener("change", function (e, target) {
    instantValidation(target);
});

for (let filterButtonLabel of sourceFormatFilterInputsArray) {
    filterButtonLabel.addEventListener("change",
        function() {
            toggleFilterButtons(this, sourceFormatFilterLabelsArray);
        });
}
//#endregion