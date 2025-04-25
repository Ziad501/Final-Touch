import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ProductListComponent } from "./product-list/product-list.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink, ProductListComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit, OnDestroy {
  currentSlide = 1;
  totalSlides = 3;
  slideInterval: any;
  router = inject(Router);
  autoSlideInterval = 5000;

  ngOnInit(): void {
    this.initSlider();
  }

  ngOnDestroy(): void {
    if (this.slideInterval) {
      clearInterval(this.slideInterval);
    }
  }

  changeSlide(slideNumber: number): void {
    clearInterval(this.slideInterval);

    for (let i = 1; i <= this.totalSlides; i++) {
      const slide = document.getElementById(`slide-${i}`);
      slide?.classList.remove('opacity-100');
      slide?.classList.add('opacity-0');

      const dot = document.getElementById(`dot-${i}`);
      if (dot) {
        dot.classList.remove('active');
        Object.assign(dot.style, {
          backgroundColor: '#ffffff',
          width: '12px',
          height: '12px',
          opacity: '0.7'
        });
      }
    }

    const activeSlide = document.getElementById(`slide-${slideNumber}`);
    activeSlide?.classList.remove('opacity-0');
    activeSlide?.classList.add('opacity-100');

    const activeDot = document.getElementById(`dot-${slideNumber}`);
    if (activeDot) {
      activeDot.classList.add('active');
      Object.assign(activeDot.style, {
        backgroundColor: '#e27b53',
        width: '16px',
        height: '16px',
        opacity: '1'
      });
    }

    this.currentSlide = slideNumber;
    this.startAutoSlide();
  }

  startAutoSlide(): void {
    this.slideInterval = setInterval(() => {
      const nextSlide = this.currentSlide >= this.totalSlides ? 1 : this.currentSlide + 1;
      this.changeSlide(nextSlide);
    }, this.autoSlideInterval);
  }

  initSlider(): void {
    this.changeSlide(1);
    this.startAutoSlide();
  }
  goToDoors() {
    this.router.navigate(['/shop'], {
      queryParams: { types: ['Interior Door'] }
    });
  }
  goToParquet() {
    this.router.navigate(['/shop'], {
      queryParams: { types: ['Parquet'] }
    });
  }
  goToMosaic() {
    this.router.navigate(['/shop'], {
      queryParams: { types: ['Mosaic'] }
    });
  }

  goToPorcelain() {
    this.router.navigate(['/shop'], {
      queryParams: { types: ['Indian Porcelain','Porcelain'] }
    });
  }
}
