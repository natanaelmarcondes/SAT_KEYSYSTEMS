// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Spinner exibe enquanto a página carrega ou quando o formulário de busca é enviado
(function () {
    function showSpinner() {
        const spinner = document.getElementById("loadingSpinner");
        if (spinner) spinner.style.display = "block";
    }

    function hideSpinner() {
        const spinner = document.getElementById("loadingSpinner");
        if (spinner) spinner.style.display = "none";
    }

    document.addEventListener("DOMContentLoaded", function () {
        // Mostrar spinner enquanto recursos terminam de carregar
        showSpinner();

        window.addEventListener("load", function () {
            // pequeno timeout para evitar flicker
            setTimeout(hideSpinner, 150);
        });

        // Se existir o formulário de busca, mostrar spinner no submit
        const searchForm = document.getElementById("searchForm");
        if (searchForm) {
            searchForm.addEventListener("submit", function () {
                showSpinner();
            });
        }
    });
})();
