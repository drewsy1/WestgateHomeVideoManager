/* global $, window */
export class View_Layout_Global {
    constructor() {
        
    }

    static goBack() {
        window.history.back();
    }

    static toggleButton(button, state) {
        if (state) {
            if (!button.classList.contains("active")) {
                button.classList.add("active");
            }
        } else {
            if (button.classList.contains("active")) {
                button.classList.remove("active");
            }
        }
        button.setAttribute("aria-pressed", state);
    }
}
