"use strict";

const App = {

    // ==============================
    // Loader
    // ==============================

    showLoader(message = "Please wait...") {

        let loader = document.getElementById("globalLoader");

        if (loader) {
            loader.querySelector(".loader-message").textContent = message;
            return;
        }

        loader = document.createElement("div");

        loader.id = "globalLoader";

        loader.innerHTML = `
            <div class="loader-overlay">
                <div class="loader-box">

                    <div class="spinner"></div>

                    <div class="loader-message">
                        ${message}
                    </div>

                </div>
            </div>
        `;

        document.body.appendChild(loader);
    },

    hideLoader() {

        const loader =
            document.getElementById("globalLoader");

        if (loader) {
            loader.remove();
        }
    },


    // ==============================
    // Message
    // ==============================

    success(message) {
        this.message(message, "success");
    },

    error(message) {
        this.message(message, "error");
    },

    warning(message) {
        this.message(message, "warning");
    },

    message(message, type = "error") {

        const old =
            document.querySelector(".app-message");

        if (old) {
            old.remove();
        }

        const element =
            document.createElement("div");

        element.className =
            `app-message ${type}`;

        element.textContent = message;

        document.body.appendChild(element);

        setTimeout(() => {
            element.remove();
        }, 3000);
    },


    // ==============================
    // Required
    // ==============================

    required(value, fieldName = "Field") {

        if (
            value === null ||
            value === undefined ||
            String(value).trim() === ""
        ) {

            return {
                valid: false,
                message: `${fieldName} is required.`
            };
        }

        return {
            valid: true
        };
    },


    // ==============================
    // Mobile
    // ==============================

    mobile(value) {

        if (!value) {

            return {
                valid: false,
                message: "Mobile number is required."
            };
        }

        const number =
            value
                .replace(/\s/g, "")
                .replace(/-/g, "");

        const pattern =
            /^(?:\+91|91)?[6-9]\d{9}$/;

        if (!pattern.test(number)) {

            return {
                valid: false,
                message: "Enter a valid mobile number."
            };
        }

        return {
            valid: true
        };
    },


    // ==============================
    // Email
    // ==============================

    email(value) {

        if (!value) {

            return {
                valid: false,
                message: "Email is required."
            };
        }

        const pattern =
            /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

        if (!pattern.test(value)) {

            return {
                valid: false,
                message: "Enter a valid email address."
            };
        }

        return {
            valid: true
        };
    },


    // ==============================
    // GUID
    // ==============================

    guid(value) {

        const pattern =
            /^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i;

        return pattern.test(value);
    },


    // ==============================
    // API Request
    // ==============================

    async request(url, options = {}) {

        try {

            this.showLoader(
                options.loaderMessage ||
                "Please wait..."
            );

            const response =
                await fetch(url, {

                    method:
                        options.method || "GET",

                    headers: {
                        "Content-Type":
                            "application/json",

                        ...(options.headers || {})
                    },

                    body:
                        options.body
                            ? JSON.stringify(options.body)
                            : null
                });


            const contentType =
                response.headers.get(
                    "content-type"
                );

            let data;

            if (
                contentType &&
                contentType.includes("application/json")
            ) {
                data = await response.json();
            }
            else {
                data = await response.text();
            }


            if (!response.ok) {

                return {
                    success: false,
                    status: response.status,
                    data
                };
            }


            return {
                success: true,
                status: response.status,
                data
            };

        }
        catch (error) {

            console.error(error);

            return {
                success: false,
                error
            };

        }
        finally {

            this.hideLoader();
        }
    },


    async get(url, options = {}) {

        return this.request(url, {
            ...options,
            method: "GET"
        });
    },


    async post(url, body, options = {}) {

        return this.request(url, {
            ...options,
            method: "POST",
            body
        });
    }
};