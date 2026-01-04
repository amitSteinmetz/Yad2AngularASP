import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoryItemModal } from './category-item-modal';

describe('CategoryItemModal', () => {
  let component: CategoryItemModal;
  let fixture: ComponentFixture<CategoryItemModal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CategoryItemModal]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CategoryItemModal);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
