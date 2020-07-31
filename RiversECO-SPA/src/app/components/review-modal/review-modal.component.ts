import { Component, OnInit } from '@angular/core';

import { BsModalRef } from 'ngx-bootstrap/modal';

import { WaterObject } from 'src/app/models/water-object';
import { Criteria } from 'src/app/models/criteria';
import { CriteriaService } from 'src/app/services/criteria.service';

@Component({
  selector: 'app-review-modal',
  templateUrl: './review-modal.component.html',
  styleUrls: ['./review-modal.component.css']
})

export class ReviewModalComponent implements OnInit {
  object: WaterObject;
  criterias: Criteria[];

  constructor(private criteriaService: CriteriaService, public bsModalRef: BsModalRef) {}

  ngOnInit() {
    this.criteriaService.getCriterias()
      .subscribe((res: Criteria[]) => {
        this.criterias = res;
      }, error => {
        console.error(error);
      });
  }

  submitReview() {
    console.log('submit review');
  }
}
