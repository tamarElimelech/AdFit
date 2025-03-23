import { Component, OnInit } from '@angular/core';
import { Page } from '../Models/page.model';
import { PageService } from '../service/page.service';
import jsPDF from 'jspdf';
import { ESize } from '../Models/advertisment.model';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-newspaper',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './newspaper.component.html',
  styleUrl: './newspaper.component.css'
})
export class NewspaperComponent implements OnInit {
  pages!: Page[];
  currentPage: Page | null = null;
  hasNextPage = false;
  hasPreviousPage = false;
  currentPageIndex = 0;

  constructor(
    private _pageService: PageService,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    this._pageService.getArrangePages().subscribe({
      next: (res) => {
        this.pages = res;
        console.log("Pages received:", this.pages);
        if (this.pages && this.pages.length > 0) {
          this.updateCurrentPage();
        }
      },
      error: (err) => {
        console.log("Error fetching pages:", err);
      }
    });
  }

  getSafeImageUrl(base64Image: string): SafeUrl {
    if (!base64Image) {
      console.log('Empty image data');
      return '';
    }

    try {
      const base64Data = base64Image.includes('base64,') 
        ? base64Image 
        : `data:image/jpeg;base64,${base64Image}`;
      return this.sanitizer.bypassSecurityTrustUrl(base64Data);
    } catch (error) {
      console.error('Error processing image:', error);
      return '';
    }
  }

  previousPage() {
    if (this.currentPageIndex > 0) {
      this.currentPageIndex--;
      this.updateCurrentPage();
    }
  }

  nextPage() {
    if (this.currentPageIndex < this.pages.length - 1) {
      this.currentPageIndex++;
      this.updateCurrentPage();
    }
  }

  downloadPDF() {
    const pdf = new jsPDF({
      orientation: 'portrait',
      unit: 'mm',
      format: 'a4'
    });
    
    this.pages.forEach((page, pageIndex) => {
      if (pageIndex > 0) {
        pdf.addPage();
      }

      // Header
      pdf.setFontSize(24);
      pdf.text('Newspaper Ads', pdf.internal.pageSize.width / 2, 20, { align: 'center' });
      pdf.setFontSize(16);
      pdf.text(`Page ${page.pageNumber}`, pdf.internal.pageSize.width / 2, 30, { align: 'center' });
      
      let yPosition = 40;
      const pageWidth = pdf.internal.pageSize.width;
      const margin = 10;
      const usableWidth = pageWidth - (2 * margin);

      page.advertisements.forEach(ad => {
        let adWidth, adHeight;
        
        // Calculate dimensions based on ad size
        switch(ad.size) {
          case ESize.HALF:
            adWidth = usableWidth;
            adHeight = usableWidth * (7/16); // Maintain aspect ratio
            break;
          case ESize.QUARTER:
            adWidth = usableWidth / 2;
            adHeight = adWidth * (3/4); // Maintain aspect ratio
            break;
          case ESize.EIGHTH:
            adWidth = usableWidth / 4;
            adHeight = adWidth; // Square
            break;
          default:
            adWidth = usableWidth;
            adHeight = adWidth * (9/16);
        }

        // Check if we need to add a new page
        if (yPosition + adHeight > pdf.internal.pageSize.height - margin) {
          pdf.addPage();
          yPosition = margin;
        }

        // Add the image
        try {
          let imageData = ad.image;
          if (!imageData.startsWith('data:image')) {
            imageData = 'data:image/jpeg;base64,' + imageData;
          }
          
          const xPosition = (pageWidth - adWidth) / 2;
          pdf.addImage(imageData, 'JPEG', xPosition, yPosition, adWidth, adHeight);
          
          // Add advertisement details
          yPosition += adHeight + 2;
          pdf.setFontSize(10);
          pdf.text(`Ad #${ad.numOfAd} - ${this.getSizeName(ad.size)} - ${ad.numOfWeeks} weeks`, 
            xPosition, yPosition);
          
          yPosition += 8; // Space for next ad
        } catch (error) {
          console.error('Error adding image to PDF:', error);
          yPosition += 10;
        }
      });
    });

    pdf.save('newspaper-ads.pdf');
  }

  getSizeClass(size: ESize): string {
    return ESize[size];
  }

  getSizeName(size: ESize): string {
    switch (size) {
      case ESize.EIGHTH: return '1/8';
      case ESize.QUARTER: return '1/4';
      case ESize.HALF: return '1/2';
      case ESize.FULL: return 'Full';
      default: return '';
    }
  }

  private updateCurrentPage() {
    this.currentPage = this.pages[this.currentPageIndex];
    this.hasPreviousPage = this.currentPageIndex > 0;
    this.hasNextPage = this.currentPageIndex < this.pages.length - 1;
    console.log('Current page updated:', this.currentPage);
  }
}