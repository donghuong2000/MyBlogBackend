window.addEventListener("scroll",function(){ 
    //các câu lệnh 
  if(pageYOffset>270)
  {
      document.getElementById("trigger").removeAttribute("hidden");
  }
  else
  {
      document.getElementById("trigger").setAttribute("hidden", true);
    }
})



function like(id) {
    $.ajax({
        method: "post",
        url: '/Client/profile/like',
        data: { Postid: id },
        success: function (data) {
            console.log(data);
        }

    })
}