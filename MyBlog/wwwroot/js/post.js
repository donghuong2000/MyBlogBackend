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