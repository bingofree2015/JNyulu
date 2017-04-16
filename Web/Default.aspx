<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="JNyuluSoft.Web._Default" %>

<script type="text/javascript">
    function jsPaginated(name, lgIDList, pageIndex) {
        $("#studentList").load("StudentList.do?name=" + name + "&lgIDList=" + lgIDList + "&pageIndex=" + pageIndex, function () {
            $(".text").editable("/Company/UpdateField",
                                {
                                    submitdata: {
                                        CompanyId: function () {
                                            return $("#CompanyId").val();
                                        },
                                        RecordType: "COMPANY"
                                    }
                                });

            $(".select").editable("/Company/UpdateField",
            {
                type: 'select',
                data: "{ 'Belgrade': 'Belgrade', 'Paris': 'Paris', 'London': 'London', 'Madrid': 'Madrid' }",
                submit: 'OK',
                submitdata: {
                    CompanyId: function () {
                        return $("#CompanyId").val();
                    },
                    RecordType: "COMPANY"
                }
            });


        });
    }
</script>
