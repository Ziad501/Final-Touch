import { Component, OnDestroy, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ProductListComponent } from "./product-list/product-list.component";

@Component({
  selector: 'app-home',
  imports: [RouterLink, ProductListComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent  implements OnInit, OnDestroy {
  currentSlide = 1;
  totalSlides = 3;
  slideInterval: any;
  autoSlideInterval = 5000; // milliseconds

  constructor() { }

  ngOnInit(): void {
    this.initSlider();
  }

  ngOnDestroy(): void {
    // Make sure to clear interval when component is destroyed
    if (this.slideInterval) {
      clearInterval(this.slideInterval);
    }
  }

  changeSlide(slideNumber: number): void {
    // Reset the timer whenever a slide is changed manually
    clearInterval(this.slideInterval);

    // Hide all slides
    for (let i = 1; i <= this.totalSlides; i++) {
      const slide = document.getElementById(`slide-${i}`);
      if (slide) {
        slide.classList.remove('opacity-100');
        slide.classList.add('opacity-0');
      }

      // Remove active class from all dots
      const dot = document.getElementById(`dot-${i}`);
      if (dot) {
        dot.classList.remove('active');
        dot.style.backgroundColor = '#ffffff';
        dot.style.width = '12px';
        dot.style.height = '12px';
        dot.style.opacity = '0.7';
      }
    }

    // Show the selected slide
    const activeSlide = document.getElementById(`slide-${slideNumber}`);
    if (activeSlide) {
      activeSlide.classList.remove('opacity-0');
      activeSlide.classList.add('opacity-100');
    }

    // Update active dot
    const activeDot = document.getElementById(`dot-${slideNumber}`);
    if (activeDot) {
      activeDot.classList.add('active');
      activeDot.style.backgroundColor = '#e27b53';
      activeDot.style.width = '16px';
      activeDot.style.height = '16px';
      activeDot.style.opacity = '1';
    }

    this.currentSlide = slideNumber;

    // Restart the auto-slide interval
    this.startAutoSlide();
  }

  startAutoSlide(): void {
    this.slideInterval = setInterval(() => {
      let nextSlide = this.currentSlide >= this.totalSlides ? 1 : this.currentSlide + 1;
      this.changeSlide(nextSlide);
    }, this.autoSlideInterval);
  }

  initSlider(): void {
    this.changeSlide(1);
    this.startAutoSlide();
  }

}
