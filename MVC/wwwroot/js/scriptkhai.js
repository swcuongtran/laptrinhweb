const allSideMenu = document.querySelectorAll('#sidebar .side-menu.top li a');

allSideMenu.forEach(item=> {
	const li = item.parentElement;

	item.addEventListener('click', function () {
		allSideMenu.forEach(i=> {
			i.parentElement.classList.remove('active');
		})
		li.classList.add('active');
	})
});




// TOGGLE SIDEBAR
const menuBar = document.querySelector('#content nav .bx.bx-menu');
const sidebar = document.getElementById('sidebar');

menuBar.addEventListener('click', function () {
	sidebar.classList.toggle('hide');
})


const searchButton = document.querySelector('#content nav form .form-input button');
const searchButtonIcon = document.querySelector('#content nav form .form-input button .bx');
const searchForm = document.querySelector('#content nav form');

searchButton.addEventListener('click', function (e) {
	if(window.innerWidth < 576) {
		e.preventDefault();
		searchForm.classList.toggle('show');
		if(searchForm.classList.contains('show')) {
			searchButtonIcon.classList.replace('bx-search', 'bx-x');
		} else {
			searchButtonIcon.classList.replace('bx-x', 'bx-search');
		}
	}
})





if(window.innerWidth < 768) {
	sidebar.classList.add('hide');
} else if(window.innerWidth > 576) {
	searchButtonIcon.classList.replace('bx-x', 'bx-search');
	searchForm.classList.remove('show');
}


window.addEventListener('resize', function () {
	if(this.innerWidth > 576) {
		searchButtonIcon.classList.replace('bx-x', 'bx-search');
		searchForm.classList.remove('show');
	}
})



const switchMode = document.getElementById('switch-mode');

switchMode.addEventListener('change', function () {
	if(this.checked) {
		document.body.classList.add('dark');
	} else {
		document.body.classList.remove('dark');
	}
})

function toggleSubMenu(event) {
    event.preventDefault(); // Ngăn chặn hành động mặc định của thẻ a
    const parentLi = event.target.closest('.has-submenu'); // Lấy phần tử cha
    const submenu = parentLi.querySelector('.submenu'); // Lấy menu con

    // Kiểm tra trạng thái của menu con và thay đổi hiển thị
    if (submenu.style.display === 'block') {
        submenu.style.display = 'none'; // Ẩn menu con
        parentLi.classList.remove('active'); // Xóa lớp active
        adjustMenuSpacing(false); // Khôi phục khoảng cách
    } else {
        submenu.style.display = 'block'; // Hiển thị menu con
        parentLi.classList.add('active'); // Thêm lớp active
        adjustMenuSpacing(true); // Điều chỉnh khoảng cách
    }
}

function toggleSubMenu(event) {
    event.preventDefault(); // Ngăn chặn hành động mặc định của thẻ a
    const parentLi = event.target.closest('.has-submenu'); // Lấy phần tử cha
    const submenu = parentLi.querySelector('.submenu'); // Lấy menu con
    const adminItem = document.querySelector('.admin-item'); // Lấy mục Admin Account

    // Kiểm tra trạng thái của menu con và thay đổi hiển thị
    if (submenu.style.display === 'block') {
        submenu.style.display = 'none'; // Ẩn menu con
        parentLi.classList.remove('active'); // Xóa lớp active
        adminItem.style.marginTop = '0'; // Khôi phục khoảng cách cho Admin Account
    } else {
        submenu.style.display = 'block'; // Hiển thị menu con
        parentLi.classList.add('active'); // Thêm lớp active
        adminItem.style.marginTop = '130px'; // Đẩy Admin Account xuống thêm
    }
}