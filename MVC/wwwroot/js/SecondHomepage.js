
//Java cho trang phụ trong trang Home 

function openModal() {
   var modal = document.getElementById("productModal");
   modal.style.display = "block";
   setTimeout(function() {
     modal.classList.add("show");
     modal.style.transform = "translate(-50%, -50%) scale(1)";
   }, 10); // Sử dụng timeout để đảm bảo hiệu ứng hoạt động
 }
 
 function closeModal() {
   var modal = document.getElementById("productModal");
   modal.classList.remove("show");
   modal.style.transform = "translate(-50%, -50%) scale(0.8)";
   setTimeout(function() {
     modal.style.display = "none";
   }, 500); // Đảm bảo đồng bộ với thời gian của hiệu ứng chuyển đổi
 }