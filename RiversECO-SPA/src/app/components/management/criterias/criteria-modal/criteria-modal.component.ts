import { Component, OnInit, EventEmitter, Output } from '@angular/core';

import { BsModalRef } from 'ngx-bootstrap/modal';

import { Criteria } from 'src/app/models/criteria';
import { CriteriaService } from 'src/app/services/criteria.service';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-criteria-modal',
  templateUrl: './criteria-modal.component.html',
  styleUrls: ['./criteria-modal.component.css']
})

export class CriteriaModalComponent implements OnInit {
  @Output() resultCriteria = new EventEmitter();

  criteria: Criteria;
  name: string;
  description: string;
  isCreateMode: boolean;

  constructor(
    private criteriaService: CriteriaService,
    private alertifyService: AlertifyService,
    public bsModalRef: BsModalRef) {}

  ngOnInit() {
    this.isCreateMode = this.criteria === undefined;
    if (!this.isCreateMode) {
      this.name = this.criteria.name;
      this.description = this.criteria.description;
    }
  }

  saveCriteria() {
    const criteriaToSave = {
      id: this.isCreateMode ? '' : this.criteria.id,
      name: this.name,
      description: this.description
    };

    if (this.isCreateMode) {
      this.criteriaService.createCriteria(criteriaToSave).subscribe(data => {
          this.resultCriteria.emit(data);
          this.alertifyService.success('Критерій "' + criteriaToSave.name + '" створено!');
          this.bsModalRef.hide();
        }, error => {
          this.alertifyService.error('Під час створення критерію виникла помилка.');
          console.error(error);
        });
    } else {
      this.criteriaService.updateCriteria(criteriaToSave).subscribe(data => {
          this.resultCriteria.emit(data);
          this.alertifyService.success('Критерій "' + criteriaToSave.name + '" оновлено!');
          this.bsModalRef.hide();
        }, error => {
          this.alertifyService.error('Під час оновлення критерію виникла помилка.');
          console.error(error);
        });
    }
  }
}
