import { Component, input, signal, OnInit, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-carousel',
  imports: [],
  templateUrl: './carousel.html',
  styleUrl: './carousel.css',
})
export class Carousel implements OnInit, OnDestroy {
  images = input.required<Array<{ path: string }>>();

  currentSlide = signal(0);
  private carouselInterval?: number;

  ngOnInit() {
    this.startCarousel();
  }

  ngOnDestroy() {
    this.stopCarousel();
  }

  startCarousel() {
    this.carouselInterval = window.setInterval(() => {
      this.nextSlide();
    }, 4000);
  }

  stopCarousel() {
    if (this.carouselInterval) {
      clearInterval(this.carouselInterval);
    }
  }

  nextSlide() {
    this.currentSlide.update((current) => (current + 1) % this.images().length);
  }

  previousSlide() {
    this.currentSlide.update(
      (current) => (current - 1 + this.images().length) % this.images().length
    );
  }

  goToSlide(index: number) {
    this.currentSlide.set(index);
    this.stopCarousel();
    this.startCarousel();
  }
}
