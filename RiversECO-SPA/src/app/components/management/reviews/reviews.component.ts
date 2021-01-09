import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';

import { ReviewModalComponent } from './review-modal/review-modal.component';
import { ReviewService } from 'src/app/services/review.service';
import { Review } from 'src/app/models/review';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.css']
})

export class ReviewsComponent implements OnInit {
  reviews: Review[];
  bsModalRef: BsModalRef;
  isLoaded: boolean;

  dataSource: MatTableDataSource<Review>;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private reviewService: ReviewService,
    private route: ActivatedRoute,
    private modalService: BsModalService) {}

  ngOnInit() {
    this.isLoaded = false;

    let method: Observable<Review[]>;
    const waterObjectId = this.route.snapshot.params['id'];

    if (waterObjectId) {
      method = this.reviewService.getReviewsForWaterObject(waterObjectId);
    } else {
      method = this.reviewService.getReviews();
    }

    method.subscribe(data => {
      this.reviews = data;

      // init table
      this.dataSource = new MatTableDataSource(this.reviews);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      this.dataSource.sortingDataAccessor = (data, sortHeaderId) => {
        switch (sortHeaderId) {
          case 'waterObject': return data.waterObject.name;
          default: return data[sortHeaderId];
        }
      };

      this.dataSource.filterPredicate = (data: Review, filter: string) => {
        if (data.createdBy.toLowerCase().includes(filter.toLowerCase())) {
          return true;
        } else if (data.waterObject.name.toLowerCase().includes(filter.toLowerCase())) {
          return true;
        } else {
          return false;
        }
      };

      this.isLoaded = true;
    }, error => {
      console.error(error);
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  openReviewModal(review: Review) {
    const options = {
      initialState: {
        waterObjectId: review.waterObject.id,
        review: review,
        isEditMode: false
      },
      animated: true,
      class: 'modal-window'
    };

    this.bsModalRef = this.modalService.show(ReviewModalComponent, options);
  }
}
