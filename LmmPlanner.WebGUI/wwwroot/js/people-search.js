"use strict";
(function () {
    const btnSuche = document.querySelector('#btnSuch');
    if(btnSuche != null) {
        btnSuche.addEventListener('click', e => {
            e.preventDefault();
            const such = document.getElementById('suche').value;
            document.querySelectorAll('tr').forEach(tr => {
                if (tr.innerHTML.indexOf(such) > -1) {
                    tr.classList.remove('d-none');
                } else {
                    tr.classList.add('d-none');
                }
            });
        })
    }
})()