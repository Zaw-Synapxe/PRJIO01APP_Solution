var DeleteBranch = function (id) {
    Swal.fire({
        title: 'Are you sure?',
        text: 'Do you want to delete this item?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes',
        showLoaderOnConfirm: false,
        allowOutsideClick: () => !swal.isLoading()
    }).then((result) => {
        if (result.value) {
            //var _BaseUrlAPIProj = BaseUrlAPIProj();
            //alert(document.location.origin + "/Branch/DeleteConfirmed/" + id);
            $.ajax({
                type: "DELETE",
                //url: _BaseUrlAPIProj[0] + "Branch/DeleteBranch/" + id,
                url: document.location.origin + "/Branch/DeleteConfirmed/" + id,
                success: function (result) {
                    var message = "Branch has been deleted successfully.";
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        didClose: () => {
                            //location.reload();
                            window.location.href = document.location.origin + "/Branch";
                        }
                    });
                }
            });
        }
    });
};


var getGitAvatar = function () {
    Swal.fire({
        title: "Submit your Github username",
        input: "text",
        inputAttributes: {
            autocapitalize: "off"
        },
        showCancelButton: true,
        confirmButtonText: "Look up",
        showLoaderOnConfirm: true,
        preConfirm: async (login) => {
            try {
                const githubUrl = `https://api.github.com/users/${login}`;
                const response = await fetch(githubUrl);
                if (!response.ok) {
                    return Swal.showValidationMessage(`${JSON.stringify(await response.json())}`);
                }
                return response.json();
            } catch (error) {
                Swal.showValidationMessage(`Request failed: ${error}`);
            }
        },
        allowOutsideClick: () => !Swal.isLoading()
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                title: `${result.value.login}'s avatar`,
                imageUrl: result.value.avatar_url
            });
        }
    });
};