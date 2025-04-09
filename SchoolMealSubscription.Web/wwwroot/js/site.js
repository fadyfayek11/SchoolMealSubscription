// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Initialize AOS animation
document.addEventListener('DOMContentLoaded', function () {
    AOS.init({
        duration: 800,
        easing: 'ease-in-out',
        once: true
    });

    // Smooth scrolling for anchor links
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();

            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                window.scrollTo({
                    top: target.offsetTop - 80,
                    behavior: 'smooth'
                });
            }
        });
    });

    // Navbar color change on scroll
    const navbar = document.querySelector('.navbar');
    window.addEventListener('scroll', function () {
        if (window.scrollY > 50) {
            navbar.classList.add('shadow');
            navbar.style.padding = '10px 0';
        } else {
            navbar.classList.remove('shadow');
            navbar.style.padding = '15px 0';
        }
    });

    // Animated counters
    const counters = document.querySelectorAll('.counter');
    const speed = 200;

    counters.forEach(counter => {
        const updateCount = () => {
            const target = +counter.getAttribute('data-target');
            const count = +counter.innerText;
            const inc = target / speed;

            if (count < target) {
                counter.innerText = Math.ceil(count + inc);
                setTimeout(updateCount, 1);
            } else {
                counter.innerText = target;
            }
        };

        updateCount();
    });

    // Add some parallax effect to hero section
    window.addEventListener('scroll', function () {
        const scrollPosition = window.pageYOffset;
        const heroSection = document.querySelector('.hero-section');
        if (heroSection) {
            heroSection.style.backgroundPositionY = scrollPosition * 0.5 + 'px';
        }
    });

    // Add hover effects for buttons
    const buttons = document.querySelectorAll('.btn');
    buttons.forEach(button => {
        button.addEventListener('mouseenter', function () {
            this.style.transform = 'translateY(-3px)';
            this.style.boxShadow = '0 5px 15px rgba(0,0,0,0.2)';
        });

        button.addEventListener('mouseleave', function () {
            this.style.transform = 'translateY(0)';
            this.style.boxShadow = 'none';
        });
    });
});