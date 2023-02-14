(function () {
    // register a-hrefs for detailed view/edit view
    function initPartDetails() {
        document.querySelectorAll('a[data-meeting-id]').forEach(a => {
            a.addEventListener('click', e => {
                const meetingId = a.dataset.meetingId;
                const type = a.dataset.type;
                const partId = 1;
                // fetch(`?handler=sched&partId=${partId}&assist=${false}&assignmentId=${assignId}`, {
                //     method: 'get'
                // }).then(resp => resp.text()).then(resp => {
                //     const pt = document.getElementById('details-part');
                //     pt.innerHTML = resp;
                //     pt.style = `max-height:${document.querySelector('.MeetingContainer').scrollHeight}px; overflow:auto;`;

                //     document.querySelectorAll('a[data-sort-header]').forEach(
                //         s => {
                //             s.addEventListener('click', ev => {
                //                 ev.preventDefault();
                //                 sortTable('personListing', s.dataset.sortHeader);
                //             })
                //         }
                //     );
                //     initBtnSavePart();
                // });
            });
        })
        document.querySelectorAll('a[data-part-id]').forEach(a => {
            a.addEventListener('click', e => {
                e.preventDefault();
                const assist = a.dataset.partAssist == 'true' ? 'True' : 'False';
                const assignId = a.dataset.assignmentId;
                fetch(`?handler=sched&partId=${a.dataset.partId}&assist=${assist}&assignmentId=${assignId}`, {
                    method: 'get'
                }).then(resp => resp.text()).then(resp => {
                    const pt = document.getElementById('details-part');
                    pt.innerHTML = resp;
                    pt.style = `max-height:${document.querySelector('.MeetingContainer').scrollHeight}px; overflow:auto;`;

                    document.querySelectorAll('a[data-sort-header]').forEach(
                        s => {
                            s.addEventListener('click', ev => {
                                ev.preventDefault();
                                sortTable('personListing', s.dataset.sortHeader);
                            })
                        }
                    );
                    initBtnSavePart();
                });
            });
        });
    }
    initPartDetails();

    const btnReload = document.getElementById('btnReload');
    btnReload.onclick = e => {
        e.preventDefault();
        const url = btnReload.href;
        fetch(url, {
            method: 'GET'
        }).then(resp => resp.text()).then(resp => {
            document.getElementById('sched-container').innerHTML = resp;
            initPartDetails();
        }).catch(err => console.error(err.toString()));
    }
    function initBtnSavePart() {
        document.querySelector('#btnSavePart').addEventListener('click', e => {
            e.preventDefault();
            const url = document.getElementById('savePartForm').action;
            console.info(url);
            const fd = new FormData();
            fd.append('partId', document.getElementById('ScheduleId').value);
            fd.append('assignmentId', document.getElementById('AssignmentId').value);
            fd.append('assigneeId', document.getElementById('savePartAssigneeId').value);
            fd.append('assist', document.getElementById('Assist').value);
            const rt = document.querySelector('input[name="__RequestVerificationToken"]').value;
            fd.append('__RequestVerificationToken', rt);
            fetch(`${url}?handler=Assignee`, {
                method: 'POST',
                body: fd
            }).then(resp => resp.json()).then(resp => {
                console.log(resp);
                if (resp) {
                    btnReload.click();
                }
            }).catch(err => console.log(err.toString()));
            btnReload.click();
        })
    }


})()