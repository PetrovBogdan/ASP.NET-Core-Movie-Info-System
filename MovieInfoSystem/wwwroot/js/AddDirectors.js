﻿let directorsIndex = 0;

$("#add-director").click(function (e) {
    e.preventDefault();
    $("#directors-container").append(`<div class="row">
        <div class="col">
            <label>First Name</label>
            <input name='Directors[`+ [directorsIndex] + `].FirstName' type='text' class='form-control' placeholder='First name' />
        </div>
        <div class="col">
            <label>Last Name</label>
            <input name='Directors[`+ [directorsIndex] + `].LastName' type='text' class='form-control' placeholder='Last name' />
        </div>
    </div>
    <br /> `);

    directorsIndex++;
});