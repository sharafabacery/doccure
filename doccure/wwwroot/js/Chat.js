/**
 * 2. Get data from Form and send by signalR to process (saved to database and send it to each user if reciver show it mark it as read)
 * 3. View all previous messages to user and update mark as read
 * 4. when user scroll update user chat messages
 * 5. show if user currently online
 * 6. exculude if user now I will talk from (AllowToTalk)
 * 7.show how many messages user cant read it until now
 * */


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
        //setTimeout(start, 5000);
    }
};
    window.onbeforeunload = function () {
        connection.stop().then(function () {
            console.log('Closed');
            connection = null;
        });

    }
    
// Start the connection.
start()

connection.on("UserConnected", (user, msg) => {
    alert(msg)
});
    connection.on("Info", (user, msg) => {
        alert(msg)
    });
    connection.on("Groups", async (user,groups) => {
        console.log(groups)
        try {
            await connection.send("GetUserGroups", groups);
        } catch (err) {
            console.error(err);
        }
});
    connection.on("UserGroups", (user, msg) => {
        var result = ``
        msg.forEach((u, index) => {
            result +=`<a  class="media chatGroupLink">
									<div class="media-img-wrap">
										<div class="avatar avatar-away">
											<img src="~/img/uploads/${u.user.image}" alt="User Image" class="avatar-img rounded-circle profileImage">
										</div>
									</div>
									<div class="media-body ">
										<div>
											<div class="user-name">Dr. ${u.user.firstName} ${u.user.lastName}</div>
<input type="text" class="form-control" name="userIdGroup" value=${u.user.id} readonly hidden>
<input type="text" class="form-control" name="groupId" value=${u.group.id} readonly hidden>
<input type="text" class="form-control" name="groupName" value=${u.group.name} readonly hidden>
<input type="text" class="form-control" name="userNameGroup" value="Dr. ${u.user.firstName} ${u.user.lastName}" readonly hidden>
											
										</div>
										
									</div>
								</a>`
        })
        $(".chatGroups").append(result);
        //alert(groupTalk)
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

    connection.on("MessagesSendClient", (user, msgs) => {
        console.log("MessagesSendClient fired")
        var chatObject = JSON.parse(localStorage.getItem('chatUser'))
        console.log(chatObject)

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
    $('.chatGroups').on("click", '.chatGroupLink', async function (e) {
        var userId = $(this).find(`input[name="userIdGroup"]`).val()
        var userName = $(this).find(`input[name="userNameGroup"]`).val()
        var groupId = $(this).find(`input[name="groupId"]`).val()
        var groupName = $(this).find(`input[name="groupName"]`).val()
        var profileImage = $(this).find('.profileImage').prop('src')

        var chatObject = {
            "userId":userId,
            "groupId": groupId,
            "groupName": groupName,
            "userName": userName,
            "profileImage": profileImage
        }
       localStorage.setItem("chatUser", JSON.stringify(chatObject))
        try {
            await connection.invoke("GetMessages", chatObject.userId, new Date());
            $(".reciver-name").text(chatObject.userName); 
            $(".reciver-img").attr("src", chatObject.profileImage);
            //$(".chat-cont-right").empty();
            //$(".chat-cont-right").append(htmlChat)
        } catch (err) {
            console.error(err);
        }
    })
    $(".chat-cont-right").submit('.MessageForm',async function (e) {
        e.preventDefault()
        var chatObject = JSON.parse(localStorage.getItem('chatUser'))

        var messageContent = $(this).find(`input[name="messageContent"]`).val();

        var obj = {
            "message": messageContent,
            "groupName": chatObject.groupName,
            "reciver": chatObject.userId,
            "uploadedFile": localStorage.getItem('path')
        }
        console.log(obj)
        await connection.invoke("AddMessage", obj);
       // alert("Submitted");
    });

    $('.ffile').change('.messageAttachment', function () {
        var fileInput = $(this).find(`input[name="messageAttachment"]`)[0];
        var file = fileInput != undefined ? fileInput.files[0] : null;

        if (file != null) {
            var form = new FormData()
            form.append('ImageFile', file)
            var form = new FormData()
            form.append('ImageFile', file)
            fetch('/ImageUploader/UploagImage/', {
                method: 'POST',
                body: form
            })
                .then(response => response.json())
                .then(data => localStorage.setItem('path', data.path))
                .catch(error => console.error('Error uploading image:', error));

        }
    })
    var chatAppTarget = $('.chat-window');
    (function () {
        if ($(window).width() > 991)
            chatAppTarget.removeClass('chat-slide');

        $(document).on("click", ".chat-window .chat-users-list a.media", function () {
            if ($(window).width() <= 991) {
                chatAppTarget.addClass('chat-slide');
            }
            return false;
        });
        $(document).on("click", "#back_user_list", function () {
            if ($(window).width() <= 991) {
                chatAppTarget.removeClass('chat-slide');
            }
            return false;
        });
    })();
})(jQuery);