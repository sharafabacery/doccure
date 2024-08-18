/**
 * 1. exculude if user now I will talk from (AllowToTalk)
 * */


(function ($) {
    "use strict";
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

    function ResetlocalStorage() {
        localStorage.removeItem('chatUser')
    }
    function isInViewport($element) {
        var elementTop = $element.offset().top;
        var elementBottom = elementTop + $element.outerHeight();

        var viewportTop = $('.chat-scroll').scrollTop();
        var viewportBottom = viewportTop + $('.chat-scroll').height();

        return elementBottom > viewportTop+10 && elementTop < viewportBottom;
    }
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
    var daysLeft = 0;
    var currentDateTracked = null
    var trackClassChatdate = 0
    var touch = false
    var MessageLeft = true
// Start the connection.
ResetlocalStorage()
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
    connection.on("UserGroups", async (user, msg) => {
        var result = ``
        msg.forEach((u, index) => {
            result +=`<a  class="media chatGroupLink">
									<div class="media-img-wrap">
										<div class="user-avatar ${u.online == true ?"avatar avatar-online":"avatar avatar-away"}">
											<img src="/img/uploads/${u.user.image}" alt="User Image" class="avatar-img rounded-circle profileImage">
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
										<div>
											
											<div class="badge badge-success badge-pill ${u.group.name}"></div>
										</div>
									</div>
								</a>`
        })
        $(".chatGroups").append(result);
        if (msg.length > 0) {
            try {
                console.log(msg)
                await connection.invoke("GetNumberOfRemianingMessages",msg);
            } catch (err) {
                console.error(err);
            }
        }
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

    connection.on("MessageSent", async(user, msg) => {
        console.log(msg)
        var chatObject = JSON.parse(localStorage.getItem('chatUser'))
        var msgBox = `<li class="${msg.receiverId != chatObject.userId ? "media sent": "media received"}"> 
            <div class="media-body">
                <div class="msg-box">
${msg.file != null ?` <div class="chat-msg-attachments">
                            <div class="chat-attachment">
                                <img src="/img/uploads/${msg.file}" alt="Attachment">
                                    <div class="chat-attach-caption">placeholder.jpg</div>
                                    <a href="/img/uploads/${msg.file}" class="chat-attach-download">
                                        <i class="fas fa-download"></i>
                                    </a>
                            </div>
                    <div>
                   
                
`:''}
               <div>
                        <p>${msg.message}</p>
                        <ul class="chat-msg-info">
                            <li>
                                <div class="chat-time">
                                    <span>${new Date(msg.createdTime).toLocaleTimeString() }</span>
                                    <span class="messageSeen messageSeen_${msg.id}">${msg.read ? "✓✓" :"✓" }</span>
                                </div>
                            </li>
                        </ul>
                    </div >
                </div>
            </div>
        </li>`
        $('.chat-cont-right').find('.list-unstyled').append(msgBox)


            var obj = {
                "reciver": msg.receiverId,
                "groupName": chatObject.groupName,
                "messageId": msg.id,
                "groupId": parseInt( chatObject.groupId)

                }
            try {
                await connection.invoke("MarkRead", obj);
            } catch (err) {
                console.error(err);
            }
    })
    connection.on('MessagesReadClient', (user, res,msgId) => {
        if (res) {
            $('.chat-cont-right').find(`.messageSeen_${msgId}`).text('✓✓')   
            var classArr = classes.split(/\s+/);
            var zz = $('#user_avatars')
            var classes = zz.attr("class");
            var classArr = classes.split(/\s+/);
            zz.removeClass("avatar-away")
            zz.addClass("avatar-online")
            $('.user-status').text(classArr.findIndex(cc => cc == "avatar-away") == -1 ? "offline" : "online")      
        }
    })
    connection.on('RemainingMessages', (_, rem) => {
        rem.forEach((re => {
            if (re.count > 0) {
                $('.chatGroups').find(`.${re.groupName}`).text(re.count)
            }
        }))
    })
    $('.ContactsGroups').on("click", '.talk', async function (e) {
        var user1 = $(this).find(`input[name="userId"]`).val()

        try {
            await connection.invoke("AddUserToGroup", user1);
        } catch (err) {
            console.error(err);
        }
       
    })
    $('.chatGroups').on("click", '.chatGroupLink', async function (e) {

        var pastChatObject = localStorage.getItem('chatUser')
        if (pastChatObject != null) {
            console.log("a7a")
            pastChatObject = JSON.parse(pastChatObject)
            try {
                await connection.invoke("ActivationUserInGroup", parseInt(pastChatObject.groupId));
            } catch (err) {
                console.log(err)
            }
        }
        //daysLeft = 0;
        currentDateTracked = null
        trackClassChatdate = 0
        touch = false
        var userId = $(this).find(`input[name="userIdGroup"]`).val()
        var userName = $(this).find(`input[name="userNameGroup"]`).val()
        var groupId = $(this).find(`input[name="groupId"]`).val()
        var groupName = $(this).find(`input[name="groupName"]`).val()
        $('.chatGroups').find(`.${groupName}`).text('')
        var profileImage = $(this).find('.profileImage').prop('src')
        var xx = $(this).find('.user-avatar')
        if (xx != undefined || xx != null) {
            var classes = xx.attr("class");
            var classArr = classes.split(/\s+/);
            var zz = $('#user_avatars')
            $('.user-status').text(classArr.findIndex(cc => cc =="avatar-online")==-1?"offline":"online")
            $.each(classArr, function (index, value) {
                zz.addClass( value)
            });
            console.log(classArr)
        }
        var chatObject = {
            "userId":userId,
            "groupId": groupId,
            "groupName": groupName,
            "userName": userName,
            "profileImage": profileImage
        }
       localStorage.setItem("chatUser", JSON.stringify(chatObject))
        try {
            //uncomment it 
            await connection.invoke("ActivationUserInGroup", parseInt(chatObject.groupId));
            $('.chat-cont-right').find('.list-unstyled').empty()
            
                var date = new Date();
                date.setDate(date.getDate() - daysLeft);
                var objDate = {
                    'reciver': chatObject.userId,
                    'date': date
                }
                console.log(objDate)

                await connection.invoke("GetMessages", objDate);
     
            
            $(".reciver-name").text(chatObject.userName); 
            $(".reciver-img").attr("src", chatObject.profileImage);
            //$(".chat-cont-right").empty();
            //$(".chat-cont-right").append(htmlChat)
        } catch (err) {
            console.error(err);
        }
    })
    connection.on('MessagesSendClient', async (u, msgs) => {
        console.log(msgs)
        var chatObject = JSON.parse(localStorage.getItem('chatUser'))
        var date = new Date();
        date.setDate(date.getDate() - daysLeft);
        currentDateTracked = date.toLocaleString()
        if (msgs == null) {
            MessageLeft = false;
            console.log('no')
            return;
        }
        var result =''
        if (msgs.length > 0) {
            result+= `<li class="chat-date chat-date-${trackClassChatdate}">${date.toLocaleString()}</li>`

        }
        msgs.forEach((msg,index) => {
            result += `<li class="${msg.receiverId != chatObject.userId ? "media sent" : "media received"}"> 
            <div class="media-body">
                <div class="msg-box">
${msg.file != null ? ` <div class="chat-msg-attachments">
                            <div class="chat-attachment">
                                <img src="/img/uploads/${msg.file}" alt="Attachment">
                                    <div class="chat-attach-caption">placeholder.jpg</div>
                                    <a href="/img/uploads/${msg.file}" class="chat-attach-download">
                                        <i class="fas fa-download"></i>
                                    </a>
                            </div>
                    <div>
                   
                
`: ''}
               <div>
                        <p>${msg.message}</p>
                        <ul class="chat-msg-info">
                            <li>
                                <div class="chat-time">
                                    <span>${new Date(msg.createdTime).toLocaleTimeString()}</span>
                                    <span class="messageSeen messageSeen_${msg.id}">${msg.read ? "✓✓" : "✓"}</span>
                                </div>
                            </li>
                        </ul>
                    </div >
                </div>
            </div>
        </li>`
        })
        
        $('.chat-cont-right').find('.list-unstyled').prepend(result)
        var obj = {
            "sender": chatObject.userId,
            "date": date
            }
        daysLeft++;
        try {
            await connection.invoke("MarkAllRead", obj);
        } catch (err) {
            console.error(err);
        }

    })
    connection.on('MarkAllRead', (_, res) => {
        if (res) {
            $('.chat-cont-right').find('.list-unstyled').find('.messageSeen').text('✓✓')
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
        localStorage.removeItem('path')
        await connection.invoke("AddMessage", obj);
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
    $('.loadmore').on('click', async () => {
        var date = new Date();
        date.setDate(date.getDate() - daysLeft);
        var chatObject = JSON.parse(localStorage.getItem('chatUser'))
        if (chatObject != null) {
            var objDate = {
                'reciver': chatObject.userId,
                'date': date
            }
            console.log(objDate)
            await connection.invoke("GetMessages", objDate);
        }

    })
    $('.chat-scroll').on('scroll', async () => {
        var targetElement = $('.chat-cont-right').find(`.chat-date-${trackClassChatdate}`);
        if (isInViewport(targetElement) && !touch && MessageLeft) {
            trackClassChatdate++;
            touch = true;
            var date = new Date();
            date.setDate(date.getDate() - daysLeft);
            var chatObject = JSON.parse(localStorage.getItem('chatUser'))
            if (chatObject != null) {
                var objDate = {
                    'reciver': chatObject.userId,
                    'date': date
                }
                console.log(objDate)
                await connection.invoke("GetMessages", objDate);
            }


        }
    })

    var chatAppTarget = $('.chat-window');
    (async function () { 
            
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
    }
        )();
})(jQuery);