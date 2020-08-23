import { Component, OnInit, EventEmitter, Output } from '@angular/core';

import { BsModalRef } from 'ngx-bootstrap/modal';

import { Criteria } from 'src/app/models/criteria';
import { CriteriaService } from 'src/app/services/criteria.service';
import { UtilsService } from 'src/app/services/utils.service';
import { Observable } from 'rxjs';

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
    private utilsService: UtilsService,
    public bsModalRef: BsModalRef) {}

  ngOnInit() {
    this.isCreateMode = this.criteria === undefined;
    if (!this.isCreateMode) {
      this.name = this.criteria.name;
      this.description = this.criteria.description;
    }
  }

  validateCriteria() {
    return !this.utilsService.isStringEmptyOrWhitespaces(this.name);
  }

  saveCriteria() {
    const criteriaToSave = {
      id: this.isCreateMode ? '' : this.criteria.id,
      name: this.name,
      description: this.description
    };

    let observableOperation: Observable<Criteria>;
    if (this.isCreateMode) {
      observableOperation = this.criteriaService.createCriteria(criteriaToSave);
    } else {
      observableOperation = this.criteriaService.updateCriteria(criteriaToSave);
    }

    observableOperation.subscribe(data => {
      this.resultCriteria.emit(data);
      this.bsModalRef.hide();
    }, error => {
      console.error(error);
    });
  }
}
