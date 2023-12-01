var imgPreview = document.getElementById('img-preview');
var deleteButton = document.getElementById('deleteButton');
var noImageSrc = 'đường_dẫn_đến_hình_mặc_định'; // Thay đổi thành đường dẫn thực tế của hình mặc định

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            imgPreview.src = e.target.result;
            deleteButton.style.display = 'block'; // Hiển thị nút "Xoá"
        };

        reader.readAsDataURL(input.files[0]);
    } else {
        imgPreview.src = noImageSrc;
        deleteButton.style.display = 'none'; // Ẩn nút "Xoá"
    }
}

function deleteImage() {
    imgPreview.src = noImageSrc;
    deleteButton.style.display = 'none'; // Ẩn nút "Xoá"
    document.getElementById('imageInput').value = ''; // Xóa giá trị của input file
}