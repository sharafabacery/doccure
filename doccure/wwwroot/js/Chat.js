(function ($) {
    "use strict";
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

connection.onclose(async () => {
    await start();
});

// Start the connection.
start()

connection.on("UserConnected", (user, msg) => {
    alert(msg)
});
    connection.on("Info", (user, msg) => {
        alert(msg)
    });
connection.on("UserGroups", (user, groups) => {
    alert(msg)
});

connection.on("AllowToTalk", (user, users) => {
    var result = ``;
    users.forEach((u, index) => {
        result +=`<a  class="media talk">
									
									<div class="media-body">
										<div>

<div class="user-name">Dr. ${u.firstName} ${u.lastName}</div>
<input type="text" class="form-control" name="userId" value=${u.id} readonly hidden>
										</div>

										
									</div>
								</a>`
    })
    $(".ContactsGroups").append(result);
});


    $('.ContactsGroups').on("click", '.talk', async function (e) {
        var user1 = $(this).find(`input[name="userId"]`).val()
        console.log(user1)
        try {
            await connection.invoke("AddUserToGroup", user1);
        } catch (err) {
            console.error(err);
        }
       
    })
})(jQuery);