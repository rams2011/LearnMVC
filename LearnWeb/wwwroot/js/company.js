let dataTable;

$(document).ready(function () {
   loadDataTable();
});

function loadDataTable() {
   dataTable = $('#tableData').DataTable({
      "ajax": { url: '/Admin/Company/getall' },
      "columns": [
         { data: 'name', "width": "35%" },
         { data: 'streetAddress', "width": "15%" },
         { data: 'city', "width": "10%" },
         { data: 'state', "width": "10%" },
         { data: 'phoneNumber', width: '10%' },
         {
            data: 'id',
            "render": function (data) {
               return `<div class="w-75 btn-group" role="group"></div>
                  <a href ="/admin/company/upsert?id=${data}" class="btn btn-primary mx-2"> <i class ="bi bi-pencil-square"></i> Edit</a>
                  <a OnClick=Delete('${data}') class="btn btn-danger mx-2"> <i class ="bi bi-trash-fill"></i> Delete</a>
                  </div>
               `
            }
         }
      ]
   });
}

function Delete(id) {
   Swal.fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes, delete it!"
   }).then((result) => {
      if (result.isConfirmed) {
         $.ajax({
            url: '/admin/company/delete/' + id,
            type: 'DELETE',
            success: function (data) {
               dataTable.ajax.reload();
               toastr.success(data.message);
            }
         })
      }
   });
}