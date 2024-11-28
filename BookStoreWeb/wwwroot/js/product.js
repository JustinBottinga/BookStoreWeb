var dataTable;

$(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#producttable').DataTable({
        'ajax': {
            'url': '/Admin/Product/GetAll'
        },
        'columns': [
            { 'data' : 'title', 'width': '30%'},
            { 'data' : 'isbn', 'width': '15%'},
            { 'data' : 'price', 'width': '10%'},
            { 'data' : 'author', 'width': '25%' },
            { 'data': 'categoryName', 'width': '20%' },
        ]
    })
}