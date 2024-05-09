## Como agregar un tipo de imágen en el proyecto.
1. La agrego al enum que se encuentra en el archivo Models/ImageData.cs 
2. La agrego al IndexOwnerUserDataViewModel
3. En el UserDataController, en el método IndexOwner(), agrego la línea, para obtener la última imágen cargada de ese campo:
```
  vm.TotalCampaigns = _userDataRepository.GetImage(ImageType.TotalCampaigns, 1);
//En este caso, TotalCampaigns es el nuevo tipo de imágen
```
4. Agrego en el IndexOwner.cshtml (de User data), antes del LightBox container lo siguiente, remplazando TotalCampaigns por el nuevo tipo:
```
<div class="container text-center col-lg-4 col-md-6 col-sm-12">
        @if (Model.TotalCampaigns != null)
        {

            <img src="@Model.TotalCampaigns.Path"
                 alt="@Model.TotalCampaigns.ImageType" class="img-fluid rounded border border-dark">
        }
        else
        {
            <img src="~/img/UserData/noImage.jpg"
                 alt="No image" class="img-fluid rounded border border-dark">
        }
        <h5 class="text-primary font-weight-bold">@Model.GetImageTypeDescription(ImageType.TotalCampaigns)</h5>
        <ul class="action-list list-unstyled list-inline">
            @if (Model.TotalCampaigns != null)
            {
                <li><a asp-controller="UserData" asp-action="UploadImageForm"  asp-route-type="@ImageType.TotalCampaigns" data-tip="add" class="list-inline-item"><i class="fa fa-plus-circle" aria-hidden="true"></i></a></li>
                <li><a asp-controller="UserData" asp-action="UpdateImageForm" asp-route-order="@Model.TotalCampaigns.Order" asp-route-type="@ImageType.TotalCampaigns" data-tip="edit" class="list-inline-item"><i class="fa fa-edit" aria-hidden="true"></i></a></li>
                <li><a asp-controller="UserData" asp-action="Delete" asp-route-order="@Model.TotalCampaigns.Order" asp-route-type="@ImageType.TotalCampaigns" data-tip="delete" class="list-inline-item"><i class="fa fa-trash" aria-hidden="true"></i></a></li>
            }
            else
            {
                <li><a asp-controller="UserData" asp-action="UploadImageForm" asp-route-type="@ImageType.TotalCampaigns" data-tip="edit" class="list-inline-item"><i class="fa fa-plus-circle" aria-hidden="true"></i></a></li>
            }
        </ul>
    </div>
</div>
```

