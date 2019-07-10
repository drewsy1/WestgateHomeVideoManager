// Realtime form validation https://www.sitepoint.com/instant-validation/

const datetimepickerMinText = document.getElementById("datetimepickerMinText");
const datetimepickerMaxText = document.getElementById("datetimepickerMaxText");
const datetimepickerError = document.getElementById("datetimepickerError");

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

function shouldBeValidated(field) {
    return (
        !(field.getAttribute("readonly") || field.readonly) &&
        !(field.getAttribute("disabled") || field.disabled)
    );
}

function instantValidation(field) {
    if (shouldBeValidated(field)) {
        var invalid =
            (datetimepickerMinText.value.length > 0) &&
            (datetimepickerMaxText.value.length > 0) &&
            (new Date(datetimepickerMaxText.value) < new Date(datetimepickerMinText.value));
        if (!invalid && field.getAttribute("aria-invalid")) {
            datetimepickerMinText.removeAttribute("aria-invalid");
            datetimepickerMaxText.removeAttribute("aria-invalid");
            datetimepickerError.style.display = "none";
        } else if (invalid && !field.getAttribute("aria-invalid")) {
            datetimepickerMinText.setAttribute("aria-invalid", "true");
            datetimepickerMaxText.setAttribute("aria-invalid", "true");
            datetimepickerError.style.display = "block";
        }
    }
}

addEvent(datetimepickerMinText, "change", function (e, target) {
    instantValidation(target);
});
addEvent(datetimepickerMaxText, "change", function (e, target) {
    instantValidation(target);
});