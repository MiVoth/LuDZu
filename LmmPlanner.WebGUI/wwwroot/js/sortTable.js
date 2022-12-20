'use strict'

function sortTable(tableName, n, isDate) {
  var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
  table = document.getElementById(tableName);
  switching = true;
  const header = table.getElementsByTagName("TH")[n];
  
  // Set the sorting direction to ascending:
  dir = "asc";
  /* Make a loop that will continue until
  no switching has been done: */
  let isAsc = true;
  let maxIters = 0;
  while (switching) {
    // Start by saying: no switching is done:
    switching = false;
    rows = table.rows;
    /* Loop through all table rows (except the
    first, which contains table headers): */
    for (i = 1; i < (rows.length - 1); i++) {
      // Start by saying there should be no switching:
      shouldSwitch = false;
      /* Get the two elements you want to compare,
      one from current row and one from the next: */
      x = rows[i].getElementsByTagName("TD")[n];
      y = rows[i + 1].getElementsByTagName("TD")[n];
      /* Check if the two rows should switch place,
      based on the direction, asc or desc: */
      let xValue = x.innerHTML.toLowerCase();
      let yValue = y.innerHTML.toLowerCase();
    //   if(isDate) {
        xValue = x.dataset.sort;
        yValue = y.dataset.sort;
        // console.log(`${xValue} ${yValue}`);
    //   }
      if (dir == "asc") {
        if (xValue > yValue) {
          // If so, mark as a switch and break the loop:
          shouldSwitch = true;
          break;
        }
      } else if (dir == "desc") {
        if (xValue < yValue) {
          // If so, mark as a switch and break the loop:
          shouldSwitch = true;
          break;
        }
      }
    }
    if (shouldSwitch) {
      /* If a switch has been marked, make the switch
      and mark that a switch has been done: */
      rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
      switching = true;
      // Each time a switch is done, increase this count by 1:
      switchcount++;
    } else {
      /* If no switching has been done AND the direction is "asc",
      set the direction to "desc" and run the while loop again. */
      if (switchcount == 0 && dir == "asc") {
        dir = "desc";
        switching = true;
        isAsc = false;
      }
    }
    if(maxIters++ > 100000) {
      break;
    }
  }
  table.querySelectorAll('.bi-chevron-down, .bi-chevron-up').forEach(f => f.className = '');
  header.classList.add(isAsc ? 'bi-chevron-up' : 'bi-chevron-down');
}