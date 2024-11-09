// For side bar 
document.querySelectorAll('.SidebarButton').forEach(button => {
   button.addEventListener('click', function() {
       document.querySelectorAll('.SidebarButton').forEach(btn => btn.classList.remove('active'));
       this.classList.add('active');
   });
});