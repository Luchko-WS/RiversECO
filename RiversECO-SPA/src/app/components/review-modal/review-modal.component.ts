import { Component, OnInit } from '@angular/core';

import { BsModalRef } from 'ngx-bootstrap/modal';

import { WaterObject } from 'src/app/models/water-object';

@Component({
  selector: 'app-review-modal',
  templateUrl: './review-modal.component.html',
  styleUrls: ['./review-modal.component.css']
})

export class ReviewModalComponent implements OnInit {
  object: WaterObject;
  criterias: any;

  constructor(public bsModalRef: BsModalRef) {}

  ngOnInit() {
  }

  submitReview() {
    console.log('submit review');
  }
}
