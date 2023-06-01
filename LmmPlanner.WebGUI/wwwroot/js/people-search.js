"use strict";
(function () {
    const btnSuche = document.querySelector('#btnSuch');
    if(btnSuche != null) {
        const sucheInput = document.getElementById('suche');
        sucheInput.addEventListener('keydown', e => {
            if(e.key.toUpperCase() == 'ENTER') {
                // console.log(e.key);
                search();
            }
        })
        btnSuche.addEventListener('click', e => {
            e.preventDefault();
            search();
            
        })
        function search() {
            const such = sucheInput.value.toLocaleLowerCase();
            document.querySelectorAll('tr').forEach(tr => {
                if (tr.innerHTML.toLocaleLowerCase().indexOf(such) > -1) {
                    tr.classList.remove('d-none');
                } else {
                    tr.classList.add('d-none');
                }
            });
        }
    }
})()