// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// To add keyboard navigation (left/right arrow keys
const lightbox = document.getElementById('lightbox') || document.getElementById('lightbox2') || document.getElementById('lightbox3');
const lightboxImg = document.getElementById('lightbox-img') || document.getElementById('lightbox-img2') || document.getElementById('lightbox-img3');
const thumbnailContainer = document.getElementById('thumbnail-container') || document.getElementById('thumbnail-container2') || document.getElementById('thumbnail-container3');

let currentIndex = 0;
const images = document.querySelectorAll('.gallery img');
const totalImages = images.length;

// Open the lightbox
function openLightbox(event) {
    if (event.target.tagName === 'IMG') {
        const clickedIndex = Array.from(images).indexOf(event.target);
        currentIndex = clickedIndex;
        CambiarOpacidad("0.1");
        updateLightboxImage();
        lightbox.style.display = 'flex';
    }
}

// Close the lightbox
function closeLightbox() {
    lightbox.style.display = 'none';
    CambiarOpacidad("1");
}

// Change the lightbox image based on direction (1 for next, -1 for prev)
function changeImage(direction) {
    currentIndex += direction;
    if (currentIndex >= totalImages) {
        currentIndex = 0;
    } else if (currentIndex < 0) {
        currentIndex = totalImages - 1;
    }
    updateLightboxImage();
}

// Update the lightbox image and thumbnails
function updateLightboxImage() {
    lightboxImg.src = images[currentIndex].src;

    thumbnailContainer.innerHTML = '';

    images.forEach((image, index) => {
        const thumbnail = document.createElement('img');
        thumbnail.src = image.src;
        thumbnail.alt = `Thumbnail ${index + 1}`;
        thumbnail.classList.add('thumbnail');
        thumbnail.addEventListener('click', () => updateMainImage(index));
        thumbnailContainer.appendChild(thumbnail);
    });

    const thumbnails = document.querySelectorAll('.thumbnail');
    thumbnails[currentIndex].classList.add('active-thumbnail');
}

// Update the main lightbox image when a thumbnail is clicked
function updateMainImage(index) {
    currentIndex = index;
    updateLightboxImage();
}

// Add initial thumbnails
updateLightboxImage();

// To add keyboard navigation (left/right arrow keys)
document.addEventListener('keydown', function (e) {
    if (lightbox.style.display === 'flex') {
        if (e.key === 'ArrowLeft') {
            changeImage(-1);
        } else if (e.key === 'ArrowRight') {
            changeImage(1);
        }
    }
});

/*ADD ERROR MESSAGE WHEN PASSWORD IS INCORRECTO*/
function CambiarOpacidad(opacity) {
    if (document.getElementById("dates-container")) {
        document.getElementById("dates-container").style.opacity = opacity;
    }
}



//Show img
function ShowImg(byteImg) {
    const blob = new Blob([bytes], { type: 'image/png' });

    // Create a temporary URL for the blob
    const imageUrl = URL.createObjectURL(blob);

    // Create an image element
    const img = new Image();
    img.src = imageUrl;

    // Append the image to the document body
    document.getElementById('image-container').appendChild(img);

    // Optionally, revoke the object URL after the image has loaded
    img.onload = function () {
        URL.revokeObjectURL(imageUrl);
    };
}
