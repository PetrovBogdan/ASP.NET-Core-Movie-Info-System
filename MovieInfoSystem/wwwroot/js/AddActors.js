﻿let actorsIndex = 0;

$("#add-actor").click(function (e) {
    e.preventDefault();
    $("#actors-container").append(`<div class="row">
        <div class="col">
            <label>First Name</label>
            <input name='Actors[`+ [actorsIndex] + `].FirstName' type='text' class='form-control' placeholder='First name' />
        </div>
        <div class="col">
            <label>Last Name</label>
            <input name='Actors[`+ [actorsIndex] + `].LastName' type='text' class='form-control' placeholder='Last name' />
        </div>
    </div>
    <br /> `);

    actorsIndex++;
});