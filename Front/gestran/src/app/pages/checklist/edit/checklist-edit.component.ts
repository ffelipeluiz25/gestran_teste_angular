import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChecklistService } from '../../../services/checklist.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-checklist-edit',
  templateUrl: './checklist-edit.component.html',
  styleUrls: ['./checklist-edit.component.css'],
  imports: [CommonModule]
})
export class ChecklistEditComponent implements OnInit {
  public formClient: FormGroup;

  constructor(private checklistService: ChecklistService, private router: Router, private fb: FormBuilder) {
    this.formClient = this.buildFormClient();
  }

  ngOnInit(): void {
  }

  private buildFormClient(): FormGroup {
    return this.fb.group({
      id: [null, Validators.required],
      name: [null, Validators.required],
      email: [null, [Validators.required, Validators.email]]
    })
  }

}