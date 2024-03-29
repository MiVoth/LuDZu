﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(function () {
    // statistics
    document.querySelectorAll('a[data-part-type]').forEach(a => {
        a.addEventListener('click', e => {
            e.preventDefault();
            const partType = a.dataset.partType;
            const fromDate = document.getElementById('FromDate').value;
            const toDate = document.getElementById('ToDate').value;
            fetch(`?handler=part&partType=${partType}&fromDate=${fromDate}&toDate=${toDate}`, {
                method: 'get'
            }).then(resp => resp.text()).then(resp => {
                const pt = document.getElementById('stat-container');
                pt.innerHTML = resp;
            });
        })
    })
    document.querySelectorAll('a[data-complete]').forEach(a => {
        a.addEventListener('click', e => {
            e.preventDefault();
            const fromDate = document.getElementById('FromDate').value;
            const toDate = document.getElementById('ToDate').value;
            fetch(`?handler=all&&fromDate=${fromDate}&toDate=${toDate}`, {
                method: 'get'
            }).then(resp => resp.text()).then(resp => {
                const pt = document.getElementById('stat-container');
                pt.innerHTML = resp;
            });
        })
    })
})()