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


function abrirModalCriar() {
    document.getElementById("tituloModalUsuario").innerText = "Novo Usuário";
    document.getElementById("formUsuario").action = "/Usuario/Create";

    document.getElementById("Id").value = "";
    document.getElementById("Nome").value = "";
    document.getElementById("Email").value = "";
    document.getElementById("Perfil").value = "USER";

    document.getElementById("areaResetSenha").classList.add("d-none");
}

function abrirModalEditar(id, nome, email, perfil) {
    document.getElementById("tituloModalUsuario").innerText = "Alterar Usuário";
    document.getElementById("formUsuario").action = "/Usuario/Edit";

    document.getElementById("Id").value = id;
    document.getElementById("Nome").value = nome;
    document.getElementById("Email").value = email;
    document.getElementById("Perfil").value = perfil;

    document.getElementById("areaResetSenha").classList.add("d-none");

    let modal = new bootstrap.Modal(document.getElementById("modalUsuario"));
    modal.show();
}

function excluirUsuario(id) {
    if (!confirm("Deseja excluir este usuário?")) return;

    fetch("/Usuario/Delete/" + id, { method: "POST" })
        .then(r => location.reload());
}

function resetarSenha(id) {
    if (!confirm("Confirmar reset da senha?")) return;

    fetch("/Usuario/ResetarSenha/" + id, { method: "POST" })
        .then(r => alert("Senha resetada para 1234."));
}
function abrirModalCriar() {
    document.getElementById("tituloModalUsuario").innerText = "Novo Usuário";
    document.getElementById("formUsuario").action = "/Usuario/Create";

    document.getElementById("Id").value = "";
    document.getElementById("Nome").value = "";
    document.getElementById("Email").value = "";
    document.getElementById("Perfil").value = "USER";

    document.getElementById("areaResetSenha").classList.add("d-none");
}

function abrirModalEditar(id, nome, email, perfil) {
    document.getElementById("tituloModalUsuario").innerText = "Alterar Usuário";
    document.getElementById("formUsuario").action = "/Usuario/Edit";

    document.getElementById("Id").value = id;
    document.getElementById("Nome").value = nome;
    document.getElementById("Email").value = email;
    document.getElementById("Perfil").value = perfil;

    document.getElementById("areaResetSenha").classList.add("d-none");

    let modal = new bootstrap.Modal(document.getElementById("modalUsuario"));
    modal.show();
}

function excluirUsuario(id) {
    if (!confirm("Deseja excluir este usuário?")) return;

    fetch("/Usuario/Delete/" + id, { method: "POST" })
        .then(r => location.reload());
}

function resetarSenha(id) {
    if (!confirm("Confirmar reset da senha?")) return;

    fetch("/Usuario/ResetarSenha/" + id, { method: "POST" })
        .then(r => alert("Senha resetada para 1234."));
}

