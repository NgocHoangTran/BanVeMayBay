$(function () {
	$(document).ready(function () {

		$("#SanBayDi,#SanBayDen,#HangVe").on('change', function (event) {
			debugger
			var sbdi = parseInt($("#SanBayDi").val());
			var sbden = parseInt($("#SanBayDen").val());
			var hangve = parseInt($("#HangVe").val());
			if (sbdi != sbden) {
				$.ajax({
					type: "GET",
					url: "/Admin/VeChuyenBays/ChoNgoi",
					data: {sbdi:sbdi,sbden:sbden,hangve:hangve},
					
					success: function (response) {

						$("#chongoi").html(response);
						$.ajax({
							type: "GET",
							url: "/Admin/VeChuyenBays/Gia",
							data: { sbdi: sbdi, sbden: sbden, hangve: hangve },

							success: function (response) {

								$("#gia").html(response);

							}
						});
					}
					
				});
				
            }
			
		});

		$("button#btndefault").click(function () {
			debugger
			var addid = $(this).data('id');
			var addname = $(this).attr('data-name');
			var addphone = $(this).attr('data-phone');
			var addaddress = $(this).attr('data-address');
			var add = {};

			add.Address_ID = addid;
			add.full_name = addname;
			add.phone = addphone;
			add.address1 = addaddress;
			add.default_address = 1;

			$.ajax({
				type: "POST",
				url: "/User/Edit",
				data: '{add: ' + JSON.stringify(add) + '}',
				contentType: "application/json; charset=utf-8",
				success: function (response) {

					$("#table-address").html(response);

				},
				error: function () {
					alert("Error");
				}
			});

		});
		$("button#btnDelete").click(function () {
			debugger
			var r = confirm("Do you want to delete?");
			var addid = $(this).data('id');
			if (r == true) {
				$.ajax({
					type: "GET",
					url: "/User/Delete",
					data: { id: addid },
					success: function (response) {

						$("#table-address").html(response);

					},
					error: function () {
						alert("Error");
					}
				});
			}

		});
	});
	function submitcreateForm() {
		var add = {};
		add.username = $('#addUsername').val();
		add.phone = $("#phone").val();
		add.address1 = $("#Address").val();
		add.email = $("#addEmail").val();
		
		$.ajax({
			type: "POST",
			url: "/User/Create",
			data: '{add: ' + JSON.stringify(add) + '}',
			contentType: "application/json; charset=utf-8",
			success: function (response) {

				$("#table-address").html(response);

			},
			error: function () {
				alert("Error");
			}
		});
	}

});