"use strict";
(function() {
    const btnPreview = document.querySelector('#btnPreview');
    btnPreview.addEventListener('click', e => {
        e.preventDefault();

        let url = '?handler=ExportPlan2'; // btnPreview.href; //'GetExportPlan';
        const fd = new FormData();
        fd.append('__RequestVerificationToken', document.querySelector('[name="__RequestVerificationToken"]').value);
        fd.append('start', document.getElementById('StartDate').value);
        fd.append('end', document.getElementById('EndDate').value);
        const start = document.getElementById('StartDate').value;
        const end = document.getElementById('EndDate').value;
        url = `${url}&start=${start}&end=${end}`;
        fetch(url, {
            method: 'GET'
            // body: fd
        }).then(r => r.json()).then(data => {
            console.log(data);
            document.getElementById('myIframe').contentDocument.body.innerHTML = data.result;
        }).catch(err => console.error(err.toString()));
    })
})()