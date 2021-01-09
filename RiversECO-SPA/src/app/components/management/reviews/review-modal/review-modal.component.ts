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
import { UtilsService } from 'src/app/services/utils.service';

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
  comment: string;
  selectedCriteriaName: string;
  references: string;
  influence?: number;
  globalInfluence?: number;

  constructor(
    private waterObjectService: WaterObjectService,
    private criteriaService: CriteriaService,
    private reviewService: ReviewService,
    private utilsService: UtilsService,
    public bsModalRef: BsModalRef) {}

  ngOnInit() {
    this.filterCriterias = this.criteriaAutocompleteControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filterCriterias(value)));

    if (this.review) {
      this.author = this.review.createdBy;
      this.comment = this.review.comment;
      this.selectedCriteriaName = this.review.criteria.name;
      this.references = this.review.references;
      this.influence = this.review.influence;
      this.globalInfluence = this.review.globalInfluence;

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

  validateReview() {
    return !this.utilsService.isStringEmptyOrWhitespaces(this.author) &&
      !this.utilsService.isStringEmptyOrWhitespaces(this.references) &&
      !this.utilsService.isStringEmptyOrWhitespaces(this.selectedCriteriaName);
  }

  submitReview() {
    const reviewToCreate = {
      createdBy: this.author,
      comment: this.comment,
      criteriaName: this.selectedCriteriaName,
      waterObjectId: this.waterObjectId,
      references: this.references,
      influence: this.influence,
      globalInfluence: this.globalInfluence
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
