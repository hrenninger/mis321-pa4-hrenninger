let app = document.getElementById("app")

let myPin = "Pin"
let url = "http://localhost:5274/api/workouts";

function handleOnLoad(){  //on page load, get entry array and set up header, modal, and table

    let html = `<div class = "page-divider"></div>
    <h1 id = "main-heading">Exercise Tracker</h1>
    <div id= "modal"></div>
    <div id="tableBody"></div>`

    document.getElementById('app').innerHTML = html;
    
    createModal();
    populateTable();
}

async function handleEntryAdd(){  //add an entry, Pin and Delete are default false
  text = document.getElementById('date').value
  textArray = text.split("-");
  formattedDate = textArray[1]+ "/" + textArray[2] + "/" + textArray[0]  //format the date
  let entry = {exercise: document.getElementById('exercise').value, distance: document.getElementById('distance').value, date: formattedDate, pinned: false, deleted: false};

  await fetch(url,{
      method: "POST",
      body: JSON.stringify(entry),
      headers:{
          "Content-type": "application/json; charset=UTF-8"
      }
  })
  populateTable();
  document.getElementById('exercise').value = ''   //clear form
  document.getElementById('distance').value = ''
  document.getElementById('date').value = ''
}

async function deleteEntry(Id, value){
  Id = Id -1;
  await fetch(url+ '/' + Id,{
      method: "DELETE",
      headers:{
          "Content-type": "application/json; charset=UTF-8"
      },
      body: JSON.stringify(value)
  })
  populateTable();
}

async function togglePin(Id, value){
  Id = Id -1;
  await fetch(url+ '/' + Id,{
      method: "PUT",
      headers:{
          "Content-type": "application/json; charset=UTF-8"
      },
      body: JSON.stringify(value)
  })
  populateTable();
}

async function populateTable(){
    let response = await fetch(url); //get the most up-to date data from sql 
    let data = await response.json();
    console.log(data)
    
    let html = `
    <table id = "exerciseTable" class="table table-spacing table-striped table-hover">
    <thead>
      <tr>
        <th class="col-sm-3">Exercise</th>
        <th class="col-sm-2">Distance (miles)</th>
        <th class="col-sm-3">Date</th>
        <th class="col-sm-2">Pin</th>
        <th class="col-sm-2">Delete</th>
      </tr>
    </thead><tbody>`

    data.forEach(function(entry){
        let value = JSON.stringify(entry)
        if(entry.distance == undefined){
            entry.distance = 0;
        }
        if(entry.pinned == true){   //Set Pin to say "Pin" or "Pinned"
          myPin = "Pinned" 
        }
        else if (entry.pinned == false){
          myPin = "Pin"
        }
        if(entry.deleted === false){   //only display entries that are not "deleted"
          html += ` 
          <tr> 
              <td>${entry.exercise}</td>
              <td>${entry.distance}</td>
              <td>${entry.date}</td>
              <td><button onclick = \'togglePin(${entry.id}, ${value})\' type="button" class="btn btn-success btn-pin">${myPin}</button></td>
              <td><button onclick = \'deleteEntry(${entry.id}, ${value})\' type="button" class="btn btn-danger btn-delete">Delete</button></td>
          </tr>`
        }
    })

    html += ` </tbody></table>`;
    document.getElementById('tableBody').innerHTML = html;
}

function createModal(){
    let html = `<div class = "btn-container"><button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">+ Add Exercise</button></div>
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true"> 
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h1 class="modal-title fs-5" id="exampleModalLabel">Add Exercise Entry</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <form onsubmit = "return false">
              <label for="exercise">Exercise:</label><br>
              <input type="text" id="exercise" name="exercise"><br>
              <label for="distance">Distance:</label><br>
              <input type="text" id="distance" name="distance"><br>
              <label for="date">Date:</label><br>
              <input type="date" id="date" name="date">
            </form>
          </div>
          <div class="modal-footer">
            <button onclick = "handleEntryAdd()" type="button" class = "btn btn-secondary" data-bs-dismiss="modal">Submit</button>
          </div>
        </div>
      </div>
    </div>
    <div style = "margin-bottom: 20px;"></div>`
    document.getElementById('modal').innerHTML = html;
}