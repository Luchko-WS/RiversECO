import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';

import { BsModalRef } from 'ngx-bootstrap/modal';

import { Review } from 'src/app/models/review';
import { WaterObject } from 'src/app/models/water-object';
import { Criteria } from 'src/app/models/criteria';
import { WaterObjectService } from 'src/app/services/water-object.service';
import { CriteriaService } from 'src/app/services/criteria.service';
import { ReviewService } from 'src/app/services/review.service';

@Component({
  selector: 'app-review-modal',
  templateUrl: './review-modal.component.html',
  styleUrls: ['./review-modal.component.css']
})

export class ReviewModalComponent implements OnInit {
  waterObjectId: string;
  review: Review;
  isEditMode: boolean;
  criterias: Criteria[];

  areCriteriasLoaded = false;
  isWaterObjectLoaded = false;
  criteriaAutocompleteControl = new FormControl();
  filterCriterias: Observable<Criteria[]>;

  waterObject: WaterObject;
  author: string;
  isAnonymous: boolean;
  comment: string;
  selectedCriteriaName: string;
  referenceType: string;
  reference: string;
  influence: string;

  constructor(
    private waterObjectService: WaterObjectService,
    private criteriaService: CriteriaService,
    private reviewService: ReviewService,
    public bsModalRef: BsModalRef) {}

  ngOnInit() {
    this.filterCriterias = this.criteriaAutocompleteControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filterCriterias(value)));

    if (this.review) {
      this.author = this.review.createdBy;
      this.comment = this.review.comment;
      this.selectedCriteriaName = this.review.criteria.name;
      this.reference = this.review.reference;
      this.influence = this.review.influence;

      // currently the modal does not support update review functionality.
      if (this.isEditMode) {
        console.error('Currently, the modal does not support update review functionality');
        this.isEditMode = false;
      }
    }

    this.waterObjectService.getWaterObject(this.waterObjectId)
      .subscribe((res: WaterObject) => {
        this.waterObject = res;
        this.isWaterObjectLoaded = true;
      }, error => {
        console.error(error);
      });

    this.criteriaService.getCriterias()
      .subscribe((res: Criteria[]) => {
        this.criterias = res;
        this.areCriteriasLoaded = true;
      }, error => {
        console.error(error);
      })
  }

  private _filterCriterias(value: string): Criteria[] {
    if (!value) {
      value = '';
    }
    if (this.criterias) {
      const filterValue = value.toLowerCase();
      return this.criterias.filter(criteria => criteria.name.toLowerCase().includes(filterValue));
    }
  }

  submitReview() {
    const reviewToCreate = {
      createdBy: this.author,
      isAnonymous: this.isAnonymous,
      waterObjectId: this.waterObjectId,
      criteriaName: this.selectedCriteriaName,
      influence: this.influence,
      referenceType: this.referenceType,
      reference: this.reference,
      comment: this.comment,
    };

    this.reviewService.createReview(reviewToCreate)
      .subscribe(review => {
          console.log('Review created.', review);
          this.bsModalRef.hide();
      }, error => {
          console.error(error);
      });
  }
}
